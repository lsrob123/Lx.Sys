using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Identity.Services.Processes;
using Lx.Utilities.Contracts.Mapping;

namespace Lx.Identity.ID3.Stores
{
    public class Id3ClientStore : IClientStore
    {
        protected readonly IClientService ClientService;
        protected readonly IMappingService MappingService;

        public Id3ClientStore(IClientService clientService, IMappingService mappingService)
        {
            ClientService = clientService;
            MappingService = mappingService;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clientDto = await Task.Run(() => ClientService.GetClientByClientId(clientId));
            var client = MappingService.Map<Client>(clientDto);
            return client;
        }
    }
}