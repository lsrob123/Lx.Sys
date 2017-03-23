namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public interface IResultBase : IDto, IRequestKey, IHasSagaId, IHasShareGroups {
        ProcessResult Result { get; set; }
        void EnsureSecurityForClientSide();
    }
}