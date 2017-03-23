﻿using System;
using System.Transactions;
using Lx.Utilities.Contract.Infrastructure.DTO;
using Lx.Utilities.Services.Infrastructure;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.NSB {
    public class NsbProgressReporter<TProgress> : ProgressReporter<TProgress>
        where TProgress : Progress, new() {
        public NsbProgressReporter(ISendOnlyBus bus, ICompletionState completionState,
            IRequestKey defaultRequestKey, Action<IProgress> setTotalCallback = null,
            Action<IProgress> reportProgressCallback = null)
            : base(completionState, defaultRequestKey, x => {
                setTotalCallback?.Invoke(x);
                using (new TransactionScope(TransactionScopeOption.Suppress)) {
                    bus.Publish(x);
                }
            }, x => {
                reportProgressCallback?.Invoke(x);
                using (new TransactionScope(TransactionScopeOption.Suppress)) {
                    bus.Publish(x);
                }
            }) {}
    }
}