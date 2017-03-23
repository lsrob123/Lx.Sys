using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IRequest : IDto, IRequestKey, IHasOriginatorIp, IHasOriginatorDevice, IHasSagaId {
        string AccessToken { get; set; }

        IdentityDto User { get; set; }

        TResponse CreateResponse<TResponse>(ProcessResult processResult = null)
            where TResponse : ResponseBase, new();
    }
}