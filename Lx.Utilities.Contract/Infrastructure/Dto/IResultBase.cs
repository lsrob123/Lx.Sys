namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IResultBase : IDto, IRequestKey, IHasSagaId, IHasShareGroups {
        ProcessResult Result { get; set; }
        void EnsureSecurityForClientSide();
    }
}