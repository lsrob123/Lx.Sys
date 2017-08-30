using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public abstract class RepublishedResponseBase<TOriginalResponse> : ResponseBase
        where TOriginalResponse : IResponse
    {
        public virtual TOriginalResponse OriginalResponse { get; set; }
    }
}