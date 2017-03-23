using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.Common {
    public interface IRequestDispatcher {
        void Dispatch(IRequest request);
    }
}