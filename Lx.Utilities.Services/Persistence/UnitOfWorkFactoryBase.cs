﻿using System;
using System.Transactions;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Enums;
using Lx.Utilities.Contracts.Infrastructure.EventBroadcasting;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Persistence;
using Lx.Utilities.Contracts.Serialization;

namespace Lx.Utilities.Services.Persistence
{
    /// <summary>
    ///     Provides base class for a generic UnitOfWork factory
    /// </summary>
    /// <typeparam name="T">A UnitOfWork type which implements <see cref="IUnitOfWork" /></typeparam>
    public abstract class UnitOfWorkFactoryBase<T> : IUnitOfWorkFactory<T> where T : IUnitOfWork
    {
        protected readonly ICacheFactory CacheFactory;
        protected readonly IEventBroadcastingProxy EventDispatchingProxy;
        protected readonly ILogger Logger;
        protected readonly IMappingService MappingService;
        protected readonly IDbConfig PrimaryDbConfig;
        protected readonly ISerializer Serializer;

        protected UnitOfWorkFactoryBase(IDbConfig primaryDbConfig, ILogger logger, ICacheFactory cacheFactory,
            IMappingService mappingService, ISerializer serializer, IEventBroadcastingProxy eventDispatchingProxy)
        {
            Logger = logger;
            CacheFactory = cacheFactory;
            MappingService = mappingService;
            PrimaryDbConfig = primaryDbConfig;
            Serializer = serializer;
            EventDispatchingProxy = eventDispatchingProxy;
        }

        /// <summary>
        ///     Method for injecting custom action which will be executed within the UnitOfWork's disposable scope, eg. scope with
        ///     DbContext
        /// </summary>
        /// <param name="action">Custom action</param>
        /// <param name="applyOuterTransactionScope">true: Wrap the UnitOfWork scope with an outer TrasactionScope</param>
        /// <param name="transactionScopeOption">TransactionScopeOption of the applied outer TrasactionScope</param>
        public virtual void Execute(Action<T> action, bool applyOuterTransactionScope = false,
            TransactionScopeOption transactionScopeOption = TransactionScopeOption.Suppress)
        {
            if (action == null)
                throw new NullReferenceException("action");

            if (applyOuterTransactionScope)
                using (var transactionScope = new TransactionScope())
                {
                    ExecuteInScope(action, true);
                    transactionScope.Complete();
                }
            else
                ExecuteInScope(action, true);
        }

        /// <summary>
        ///     Method for injecting custom action which will be executed within the UnitOfWork's disposable scope, eg. scope with
        ///     DbContext
        /// </summary>
        /// <param name="action">Custom action</param>
        /// <param name="applyOuterTransactionScope">true: Wrap the UnitOfWork scope with an outer TrasactionScope</param>
        /// <param name="transactionScopeOption">TransactionScopeOption of the applied outer TrasactionScope</param>
        /// <returns>ProcessResult instance which could include handled exceptions</returns>
        public virtual ProcessResult ExecuteWithProcessResult(Action<T> action, bool applyOuterTransactionScope = false,
            TransactionScopeOption transactionScopeOption = TransactionScopeOption.Suppress)
        {
            if (action == null)
                return new NullReferenceException("action");

            ProcessResult result;
            if (applyOuterTransactionScope)
                using (var transactionScope = new TransactionScope(transactionScopeOption))
                {
                    result = ExecuteInScope(action);
                    transactionScope.Complete();
                }
            else
                result = ExecuteInScope(action);

            return result;
        }

        /// <summary>
        ///     Abstract method to be implemented to get an instance of the UnitOfWork of the specified generic type
        /// </summary>
        /// <returns></returns>
        protected abstract T GetUnitOfWork();

        protected virtual ProcessResult ExecuteInScope(Action<T> action, bool rethrowException = false)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                try
                {
                    action(unitOfWork);
                    return ProcessResultType.Ok;
                }
                catch (Exception exception)
                {
                    Logger.LogException(
                        new Exception(
                            $"<<{GetType().AssemblyQualifiedName} is throwing exception {exception.GetType().FullName}>>",
                            exception));

                    if (rethrowException)
                        throw;

                    return exception;
                }
            }
        }

        protected virtual void DispatchEvent<TEvent>(TEvent eventObject) where TEvent : ResultBase
        {
            EventDispatchingProxy.Broadcast(eventObject, EventBroadcastingScope.CrossProcesses);
        }
    }
}