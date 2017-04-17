using Lx.Identity.Contracts.DTOs;
using Lx.Shared.All.Identity.Interfaces;

namespace Lx.Identity.Services.Services {
    public interface IClientService {
        ClientDto GetClientByClientId(string clientId);
        IHasUserProfileOriginator GetAssociatedUserProfileOriginator(string clientId);
    }
}