using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;

namespace Lx.Identity.Services.Services
{
    public interface IConsentService
    {
        void AddOrUpdateConsent(ConsentDto consent);
        ICollection<ConsentDto> ListConsents(string subjectId);
        ConsentDto GetConsent(string subjectId, string clientId);
        void RemoveConsent(string subjectId, string clientId);
    }
}