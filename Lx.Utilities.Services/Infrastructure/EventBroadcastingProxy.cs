﻿using System;
using System.Collections.Generic;
using System.Threading;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.EventBroadcasting;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Utilities.Services.Infrastructure {
    public class EventBroadcastingProxy : IEventBroadcastingProxy {
        protected static readonly SortedDictionary<Type, IEventBroadcaster> BroadcasterLookUps =
            new SortedDictionary<Type, IEventBroadcaster>();

        protected static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        protected readonly IBus Bus;

        public EventBroadcastingProxy(IBus bus) {
            Bus = bus;

            Register(new NsbEventBroadcaster(Bus));
        }

        public void Broadcast<TEvent>(TEvent e) where TEvent : ResponseBase {
            var broadcasters = new List<IEventBroadcaster>();
            Lock.EnterReadLock();
            try {
                broadcasters.AddRange(BroadcasterLookUps.Values);
            } finally {
                Lock.ExitReadLock();
            }

            foreach (var broadcaster in broadcasters)
                broadcaster.Broadcast(e);
        }

        public void Register<TDispatcher>(TDispatcher broadcaster) where TDispatcher : IEventBroadcaster {
            Lock.EnterUpgradeableReadLock();
            try {
                var broadcasterType = broadcaster.GetType();
                if (BroadcasterLookUps.ContainsKey(broadcasterType))
                    return;

                Lock.EnterWriteLock();
                try {
                    BroadcasterLookUps.Add(broadcasterType, broadcaster);
                } finally {
                    Lock.ExitWriteLock();
                }
            } finally {
                Lock.ExitUpgradeableReadLock();
            }
        }
    }
}