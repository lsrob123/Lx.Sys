using System.Collections.Generic;
using System.Linq;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;
using Lx.Identity.Persistence.Uow;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Identity.Services.Services
{
    public class ConsentService : IConsentService
    {
        protected readonly IMappingService MappingService;
        protected readonly IOAuthUowFactory OAuthUowFactory;

        public ConsentService(IMappingService mappingService, IOAuthUowFactory oAuthUowFactory)
        {
            MappingService = mappingService;
            OAuthUowFactory = oAuthUowFactory;
        }

        public void AddOrUpdateConsent(ConsentDto consentDto)
        {
            var consent = MappingService.Map<Consent>(consentDto);
            OAuthUowFactory.AddOrUpdateConsent(consent);
        }

        public ICollection<ConsentDto> ListConsents(string subjectId)
        {
            var consentDtos = OAuthUowFactory.ListConsents(subjectId)
                .Select(x => MappingService.Map<ConsentDto>(x))
                .ToList();
            return consentDtos;
        }

        public ConsentDto GetConsent(string subjectId, string clientId)
        {
            var consentDto = MappingService.Map<ConsentDto>(OAuthUowFactory.GetConsent(subjectId, clientId));
            return consentDto;
        }

        public void RemoveConsent(string subjectId, string clientId)
        {
            OAuthUowFactory.RemoveConsent(subjectId, clientId);
        }
    }
}