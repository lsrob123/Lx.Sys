using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.RequestDispatching
{
    public interface IRequestDispatcherBase
    {
        void Dispatch<TRequest>(TRequest request) where TRequest : IRequest;
    }
}