using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IResultBase : IDto, IRequestKey, IHasSagaId, IHasShareGroups
    {
        ProcessResult Result { get; set; }
        void EnsureSecurityForClientSide();
    }
}