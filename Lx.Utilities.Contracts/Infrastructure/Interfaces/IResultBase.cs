using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IResultBase : IDto, IRequestKey, IHasSagaId, IHasShareGroups
    {
        ProcessResult Result { get; set; }
        void EnsureSecurityForClientSide();
    }
}