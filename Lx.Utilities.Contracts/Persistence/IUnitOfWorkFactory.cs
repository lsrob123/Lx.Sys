﻿using System;
using System.Transactions;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Persistence
{
    /// <summary>
    ///     Provides interface for a generic UnitOfWork factory
    /// </summary>
    /// <typeparam name="T">A UnitOfWork type which implements <see cref="IUnitOfWork" /></typeparam>
    public interface IUnitOfWorkFactory<out T> where T : IUnitOfWork
    {
        /// <summary>
        ///     Method for injecting custom action which will be executed within the UnitOfWork's disposable scope, eg. scope with
        ///     DbContext
        /// </summary>
        /// <param name="action">Custom action</param>
        /// <param name="applyOuterTransactionScope">true: Wrap the UnitOfWork scope with an outer TrasactionScope</param>
        /// <param name="transactionScopeOption"></param>
        void Execute(Action<T> action, bool applyOuterTransactionScope = false,
            TransactionScopeOption transactionScopeOption = TransactionScopeOption.Suppress);

        /// <summary>
        ///     Method for injecting custom action which will be executed within the UnitOfWork's disposable scope, eg. scope with
        ///     DbContext
        /// </summary>
        /// <param name="action">Custom action</param>
        /// <param name="applyOuterTransactionScope">true: Wrap the UnitOfWork scope with an outer TrasactionScope</param>
        /// <param name="transactionScopeOption">TransactionScopeOption of the applied outer TrasactionScope</param>
        /// <returns>ProcessResult instance which could include handled exceptions</returns>
        ProcessResult ExecuteWithProcessResult(Action<T> action, bool applyOuterTransactionScope = false,
            TransactionScopeOption transactionScopeOption = TransactionScopeOption.Suppress);
    }
}