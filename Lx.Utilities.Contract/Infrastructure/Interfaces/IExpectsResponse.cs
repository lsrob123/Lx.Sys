namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IExpectsResponse<out T> where T : IResponse
    {
        T CreateResponse();
    }
}