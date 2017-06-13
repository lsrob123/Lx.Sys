using System;
using System.Collections.Generic;
using System.Threading;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.RequestDispatching;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Utilities.Services.Infrastructure
{
    public class RequestDispatchingProxy : IRequestDispatchingProxy
    {
        protected static readonly SortedDictionary<Type, IRequestDispatcher> DispatcherLookUps =
            new SortedDictionary<Type, IRequestDispatcher>();

        protected static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        protected readonly IBus Bus;
        protected readonly IMediator Mediator;

        public RequestDispatchingProxy(IMediator mediator, IBus bus, IBusSettings busSettings,
            IBusEndpointMapFactory exceptionalBusEndpointMapFactory)
        {
            Mediator = mediator;
            Bus = bus;

            RegisterDispatcher(new NsbRequestDispatcher(bus, busSettings, exceptionalBusEndpointMapFactory));
        }

        public void Dispatch<TRequest>(TRequest e) where TRequest : IRequest
        {
            var dispatchers = new List<IRequestDispatcher>();
            Lock.EnterReadLock();
            try
            {
                dispatchers.AddRange(DispatcherLookUps.Values);
            }
            finally
            {
                Lock.ExitReadLock();
            }

            foreach (var dispatcher in dispatchers)
                dispatcher.Dispatch(e);
        }

        public void RegisterDispatcher<TDispatcher>(TDispatcher dispatcher) where TDispatcher : IRequestDispatcher
        {
            Lock.EnterUpgradeableReadLock();
            try
            {
                var dispatcherType = dispatcher.GetType();
                if (DispatcherLookUps.ContainsKey(dispatcherType))
                    return;

                Lock.EnterWriteLock();
                try
                {
                    DispatcherLookUps.Add(dispatcherType, dispatcher);
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }
            finally
            {
                Lock.ExitUpgradeableReadLock();
            }
        }
    }
}