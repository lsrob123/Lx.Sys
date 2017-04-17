using System;
using System.Threading;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Services.Infrastructure
{
    /// <summary>
    ///     ProgressReporter (Thread safe progress reporter)
    /// </summary>
    public class ProgressReporter<TProgress> : IProgressReporter<TProgress>
        where TProgress : Progress, new()
    {
        protected readonly IRequestKey DefaultRequestKey;
        protected readonly object ProgressReportingLock = new object();
        protected readonly Action<IProgress> ReportProgressCallback;
        protected readonly Action<IProgress> SetTotalCallback;

        /// <summary>
        ///     ProgressReporter (Thread safe progress reporter)
        /// </summary>
        /// <param name="completionState">Associated state object which implements ICompletionState</param>
        /// <param name="defaultRequestKey">Optional IRequestKey object which can be attached to the progress object</param>
        /// <param name="setTotalCallback">Optional callback executed after setting value of the Total property</param>
        /// <param name="reportProgressCallback">Optional callback executed after setting value of the Total property</param>
        public ProgressReporter(ICompletionState completionState, IRequestKey defaultRequestKey,
            Action<IProgress> setTotalCallback = null, Action<IProgress> reportProgressCallback = null)
        {
            LinkTo(completionState);
            DefaultRequestKey = defaultRequestKey;
            SetTotalCallback = setTotalCallback;
            ReportProgressCallback = reportProgressCallback;
        }

        public ICompletionState CompletionState { get; protected set; }

        public void LinkTo(ICompletionState completionState)
        {
            CompletionState = completionState;
        }

        public TProgress CreateProgress(IRequestKey requestKey, Action<TProgress> extraActionOnProgressObject,
            object data = null)
        {
            if (CompletionState == null)
                return null;

            var progress = CompletionState as TProgress ?? new TProgress()
                .LinkTo(requestKey ?? DefaultRequestKey)
                .WithUpdate(CompletionState.ProgressCompleted, CompletionState.ProgressTotal)
                .WithMessage(CompletionState.ProgressMessage);

            extraActionOnProgressObject?.Invoke(progress);

            return progress;
        }

        public virtual void SetTotal(decimal total, Action<TProgress> extraActionOnProgressObject, string message = null,
            object data = null,
            IRequestKey requestKey = null)
        {
            CheckCompletionStateAvailability();

            Monitor.Enter(ProgressReportingLock);
            try
            {
                CompletionState.ProgressTotal = total;
                CompletionState.ProgressMessage = message;

                SetTotalCallback?.Invoke(CreateProgress(requestKey, extraActionOnProgressObject, data));
            }
            finally
            {
                Monitor.Exit(ProgressReportingLock);
            }
        }

        public virtual void Report(decimal increment, Action<TProgress> extraActionOnProgressObject,
            string message = null, object data = null,
            IRequestKey requestKey = null)
        {
            CheckCompletionStateAvailability();

            Monitor.Enter(ProgressReportingLock);
            try
            {
                CompletionState.ProgressCompleted += increment;
                CompletionState.ProgressMessage = message;
                ReportProgressCallback?.Invoke(CreateProgress(requestKey, extraActionOnProgressObject, data));
            }
            finally
            {
                Monitor.Exit(ProgressReportingLock);
            }
        }

        public void Report(object data, Action<TProgress> extraActionOnProgressObject, string message = null,
            IRequestKey requestKey = null)
        {
            Report(0, extraActionOnProgressObject, message, data, requestKey);
        }

        public void EnforceCompletion(Action<TProgress> extraActionOnProgressObject, string message = null,
            object data = null, IRequestKey requestKey = null)
        {
            CheckCompletionStateAvailability();

            Monitor.Enter(ProgressReportingLock);
            try
            {
                if (CompletionState.ProgressCompleted == CompletionState.ProgressTotal)
                    return;

                CompletionState.ProgressCompleted = CompletionState.ProgressTotal;
                CompletionState.ProgressMessage = message;

                ReportProgressCallback?.Invoke(CreateProgress(requestKey, extraActionOnProgressObject, data));
            }
            finally
            {
                Monitor.Exit(ProgressReportingLock);
            }
        }

        protected virtual void CheckCompletionStateAvailability()
        {
            if (CompletionState == null)
                throw new NullReferenceException(
                    $"This ProgressReporter instance has not been associated with a {nameof(CompletionState)} object yet.");
        }
    }
}