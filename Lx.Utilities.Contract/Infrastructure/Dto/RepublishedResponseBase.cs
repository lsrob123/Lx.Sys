namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public abstract class RepublishedResponseBase<TOriginalResponse> : ResponseBase
        where TOriginalResponse : IResponse {
        public virtual TOriginalResponse OriginalResponse { get; set; }
    }
}