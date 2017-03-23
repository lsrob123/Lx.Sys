using System;
using Lx.Utilities.Contract.Infrastructure.DTO;

namespace Lx.Utilities.Contract.Infrastructure.Common {
    public interface IProgressReporter<out TProgress> where TProgress : Progress, new() {
        ICompletionState CompletionState { get; }
        void LinkTo(ICompletionState completionState);

        TProgress CreateProgress(IRequestKey requestKey, Action<TProgress> extraActionOnProgressObject,
            object data = null);

        void SetTotal(decimal total, Action<TProgress> extraActionOnProgressObject, string message = null,
            object data = null, IRequestKey requestKey = null);

        void Report(decimal increment, Action<TProgress> extraActionOnProgressObject, string message = null,
            object data = null, IRequestKey requestKey = null);

        void Report(object data, Action<TProgress> extraActionOnProgressObject, string message = null,
            IRequestKey requestKey = null);

        void EnforceCompletion(Action<TProgress> extraActionOnProgressObject, string message = null, object data = null,
            IRequestKey requestKey = null);
    }
}