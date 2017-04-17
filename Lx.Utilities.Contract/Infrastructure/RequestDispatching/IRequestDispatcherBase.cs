using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.RequestDispatching
{
    public interface IRequestDispatcherBase
    {
        void Dispatch<TRequest>(TRequest request) where TRequest : IRequest;
    }
}