using Lx.Utilities.Contract.Authentication.DTOs;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public interface IRequest : IDto, IRequestKey, IHasOriginatorIp, IHasOriginatorDevice, IHasSagaId {
        string AccessToken { get; set; }

        IdentityDto User { get; set; }

        TResponse CreateResponse<TResponse>(ProcessResult processResult = null)
            where TResponse : ResponseBase, new();
    }
}