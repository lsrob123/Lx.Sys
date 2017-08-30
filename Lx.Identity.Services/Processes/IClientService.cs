using Lx.Identity.Contracts.DTOs;

namespace Lx.Identity.Services.Processes
{
    public interface IClientService
    {
        ClientDto GetClientByClientId(string clientId);
    }
}