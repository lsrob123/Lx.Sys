using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;

namespace Lx.Identity.Persistence.Uow
{
    public interface IOAuthUowFactory
    {
        ClientDto AddOrUpdateClient(ClientDto clientDto);
        ClientDto GetClient(string clientId);
        ScopeDto AddOrUpdateScope(ScopeDto scopeDto);
        ICollection<ScopeDto> ListScopes(IEnumerable<string> scopeNames = null);
        void AddOrUpdateConsent(Consent consent);
        ICollection<Consent> ListConsents(string subjectId);
        Consent GetConsent(string subjectId, string clientId);
        void RemoveConsent(string subjectId, string clientId);
    }
}