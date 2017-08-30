using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;

namespace Lx.Identity.Services.Processes
{
    public interface IConsentService
    {
        void AddOrUpdateConsent(ConsentDto consentDto);
        ICollection<ConsentDto> ListConsents(string subjectId);
        ConsentDto GetConsent(string subjectId, string clientId);
        void RemoveConsent(string subjectId, string clientId);
    }
}