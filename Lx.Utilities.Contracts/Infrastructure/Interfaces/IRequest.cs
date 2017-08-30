using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IRequest : IDto, IRequestKey, IHasOriginatorIp, IHasOriginatorDevice, IHasSagaId
    {
        string AccessToken { get; set; }
        IdentityDto User { get; set; }

        TResponse CreateResponse<TResponse>(ProcessResult processResult = null)
            where TResponse : ResponseBase, new();

        TEvent CreateEvent<TEvent>(ProcessResult processResult = null)
            where TEvent : EventBase, new();
    }
}