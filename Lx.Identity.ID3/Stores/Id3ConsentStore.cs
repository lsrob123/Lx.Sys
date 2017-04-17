using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Identity.Contracts.DTOs;
using Lx.Utilities.Contract.Mapping;
using IConsentService = Lx.Identity.Services.Services.IConsentService;

namespace Lx.Identity.ID3.Stores
{
    public class Id3ConsentStore : IConsentStore
    {
        protected readonly IConsentService ConsentService;
        protected readonly IMappingService MappingService;

        public Id3ConsentStore(IMappingService mappingService, IConsentService consentService)
        {
            MappingService = mappingService;
            ConsentService = consentService;
        }

        public async Task<IEnumerable<Consent>> LoadAllAsync(string subject)
        {
            var consents = await Task.Run(() => ConsentService
                .ListConsents(subject)
                .Select(x => MappingService.Map<Consent>(x))
                );
            return consents;
        }

        public async Task RevokeAsync(string subject, string client)
        {
            await Task.Run(() => ConsentService.RemoveConsent(subject, client));
        }

        public async Task<Consent> LoadAsync(string subject, string client)
        {
            var consentEntity = await Task.Run(() => ConsentService.GetConsent(subject, client));
            if (consentEntity == null)
                return null;

            var consent = MappingService.Map<Consent>(consentEntity);
            return consent;
        }

        public async Task UpdateAsync(Consent consent)
        {
            var consentDto = MappingService.Map<ConsentDto>(consent);
            await Task.Run(() => ConsentService.AddOrUpdateConsent(consentDto));
        }
    }
}