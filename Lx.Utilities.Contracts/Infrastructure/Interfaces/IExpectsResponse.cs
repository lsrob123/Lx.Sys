namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IExpectsResponse<out T> where T : IResponse
    {
        T CreateResponse();
    }
}