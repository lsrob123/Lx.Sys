using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Persistence.Uow;

namespace Lx.Identity.Services.Processes
{
    public class ClientService : IClientService
    {
        protected readonly IOAuthUowFactory OAuthUowFactory;

        public ClientService(IOAuthUowFactory oAuthUowFactory)
        {
            OAuthUowFactory = oAuthUowFactory;
        }

        public ClientDto GetClientByClientId(string clientId)
        {
            var clientDto = OAuthUowFactory.GetClient(clientId);
            return clientDto;
        }
    }
}