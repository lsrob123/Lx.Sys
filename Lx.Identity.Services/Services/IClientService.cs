using Lx.Identity.Contracts.DTOs;

namespace Lx.Identity.Services.Services
{
    public interface IClientService
    {
        ClientDto GetClientByClientId(string clientId);
    }
}