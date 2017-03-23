namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IExpectsResponse<out T> where T : IResponse {
        T CreateResponse();
    }
}