using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public abstract class RepublishedResponseBase<TOriginalResponse> : ResponseBase
        where TOriginalResponse : IResponse {
        public virtual TOriginalResponse OriginalResponse { get; set; }
    }
}