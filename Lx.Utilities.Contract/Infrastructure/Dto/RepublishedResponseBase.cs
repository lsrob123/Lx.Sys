namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public abstract class RepublishedResponseBase<TOriginalResponse> : ResponseBase
        where TOriginalResponse : IResponse {
        public virtual TOriginalResponse OriginalResponse { get; set; }
    }
}