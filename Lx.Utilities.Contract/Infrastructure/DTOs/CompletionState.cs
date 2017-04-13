using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public class CompletionState : ResponseBase, ICompletionState {
        public CompletionState() {}

        public CompletionState(IBasicRequestKey request, string progressMessage = null) {
            if (request != null)
                this.LinkTo(request);

            SetProgressMessage(progressMessage);
        }

        public virtual decimal ProgressTotal { get; set; }
        public virtual decimal ProgressCompleted { get; set; }
        public virtual string ProgressMessage { get; set; }

        protected void SetProgressMessage(string progressMessage) {
            ProgressMessage = progressMessage;
        }

        public override void EraseShareGroupInfoForClientSide() {}
    }
}