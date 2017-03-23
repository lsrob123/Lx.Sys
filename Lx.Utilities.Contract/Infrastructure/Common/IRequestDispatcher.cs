using Lx.Utilities.Contract.Infrastructure.DTO;

namespace Lx.Utilities.Contract.Infrastructure.Common {
    public interface IRequestDispatcher {
        void Dispatch(IRequest request);
    }
}