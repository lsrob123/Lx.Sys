namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IExpectsResponse<out T> where T : IResponse {
        T CreateResponse();
    }
}