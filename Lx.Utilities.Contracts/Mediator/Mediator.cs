using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Lx.Utilities.Contracts.Configuration;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Mediator
{
    public class Mediator : IMediator
    {
        protected static IMediator DefaultInstance;

        protected static readonly Dictionary<Type, List<IMediatorMessageHandler>> Handlers =
            new Dictionary<Type, List<IMediatorMessageHandler>>();

        protected static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        protected static readonly Type MediatorType = typeof(Mediator);
        protected static readonly Type MediatorMessageHandlerType = typeof(IMediatorMessageHandler);
        public static IMediator Default => DefaultInstance;

        public async Task<IMediator> PublishAsync<T>(T message)
            where T : IMessageBase
        {
            return await Task.Run(() => Publish(message));
        }

        public virtual IMediator Publish<T>(T message)
            where T : IMessageBase
        {
            var eventType = message.GetType(); //TODO: review to see if T can be used instead
            List<IMediatorMessageHandler> handlers;
            Lock.EnterReadLock();
            try
            {
                Handlers.TryGetValue(eventType, out handlers);
            }
            finally
            {
                Lock.ExitReadLock();
            }

            if (handlers != null)
                Parallel.ForEach(handlers, handler => ExecuteHandler(handler, message));
            return this;
        }

        public virtual IMediator Subscribe<T>(IMediatorMessageHandler<T> handler)
            where T : IMessageBase
        {
            var eventType = typeof(T);

            Lock.EnterUpgradeableReadLock();
            try
            {
                List<IMediatorMessageHandler> handlers;
                if (!Handlers.TryGetValue(eventType, out handlers))
                {
                    Lock.EnterWriteLock();
                    try
                    {
                        Handlers.Add(eventType, new List<IMediatorMessageHandler> {handler});
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }
                }
                else if (handlers.All(x => x != handler))
                {
                    Lock.EnterWriteLock();
                    try
                    {
                        handlers.Add(handler);
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                Lock.EnterUpgradeableReadLock();
            }

            return this;
        }

        public void RegisterAllHandlers(object o)
        {
            var objectType = o.GetType();
            var methodInfo = MediatorType.GetMethod(nameof(RegisterHandler),
                BindingFlags.Instance | BindingFlags.NonPublic);

            //var mediatorMessageTypes = objectType.GetInterfaces()
            //    .Where(x => MediatorMessageHandlerType.IsAssignableFrom(x) &&
            //                (x.GenericTypeArguments != null) &&
            //                x.GenericTypeArguments.Any())
            //    .Select(x => x.GenericTypeArguments.First())
            //    .ToList();

            //foreach (var mediatorMessageType in mediatorMessageTypes)
            //{
            //    var method = methodInfo.MakeGenericMethod(mediatorMessageType);
            //    method.Invoke(this, new[] {o});
            //}

            var methods = objectType.GetInterfaces()
                .Where(x => MediatorMessageHandlerType.IsAssignableFrom(x) &&
                            x.GenericTypeArguments != null &&
                            x.GenericTypeArguments.Any())
                .Select(x => methodInfo.MakeGenericMethod(x.GenericTypeArguments.First()))
                .ToList();

            foreach (var method in methods)
                method.Invoke(this, new[] {o});
        }

        protected void ExecuteHandler<T>(IMediatorMessageHandler handler, T message) where T : IMessageBase
        {
            ((IMediatorMessageHandler<T>) handler)?.Handle(message);
        }

        protected void RegisterHandler<TMessage>(object o)
            where TMessage : IMessageBase
        {
            Subscribe(o as IMediatorMessageHandler<TMessage>);
        }

        [Preconfiguration]
        public static void CreateDefaultInstance()
        {
            DefaultInstance = new Mediator();
            DefaultInstance.PublishAsync(new MediatorReadyEvent()).Wait();
        }
    }
}