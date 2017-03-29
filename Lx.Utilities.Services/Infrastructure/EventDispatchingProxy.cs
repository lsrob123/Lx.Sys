using System;
using System.Collections.Generic;
using System.Threading;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.EventDispacthing;
using Lx.Utilities.Contract.Mediator;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Utilities.Services.Infrastructure {
    public class EventDispatchingProxy : IEventDispatchingProxy {
        protected static readonly SortedDictionary<Type, IEventDispatcher> DispatcherLookUps =
            new SortedDictionary<Type, IEventDispatcher>();

        protected static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        protected readonly IBus Bus;

        protected readonly IMediator Mediator;

        public EventDispatchingProxy(IMediator mediator, IBus bus) {
            Mediator = mediator;
            Bus = bus;

            RegisterDispatcher(new NsbEventDispatcher(Bus));
        }

        public void Dispatch<TEvent>(TEvent e) where TEvent : ResponseBase {
            var dispatchers = new List<IEventDispatcher>();
            Lock.EnterReadLock();
            try {
                dispatchers.AddRange(DispatcherLookUps.Values);
            } finally {
                Lock.ExitReadLock();
            }

            foreach (var dispatcher in dispatchers)
                dispatcher.Dispatch(e);
        }

        public void RegisterDispatcher<TDispatcher>(TDispatcher dispatcher) where TDispatcher : IEventDispatcher {
            Lock.EnterUpgradeableReadLock();
            try {
                var dispatcherType = dispatcher.GetType();
                if (DispatcherLookUps.ContainsKey(dispatcherType))
                    return;

                Lock.EnterWriteLock();
                try {
                    DispatcherLookUps.Add(dispatcherType, dispatcher);
                } finally {
                    Lock.ExitWriteLock();
                }
            } finally {
                Lock.ExitUpgradeableReadLock();
            }
        }
    }
}