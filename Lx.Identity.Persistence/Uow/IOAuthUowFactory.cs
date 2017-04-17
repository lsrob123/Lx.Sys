using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;
using Lx.Identity.Domain.Entities;

namespace Lx.Identity.Persistence.Uow
{
    public interface IOAuthUowFactory
    {
        void AddOrUpdateClient(ClientDto clientDto);
        ClientDto GetClient(string clientId);
        void AddOrUpdateScope(Scope scope);
        IReadOnlyCollection<Scope> ListScopes(IEnumerable<string> scopeNames = null);
        void AddOrUpdateConsent(Consent consent);
        IReadOnlyCollection<Consent> ListConsents(string subjectId);
        Consent GetConsent(string subjectId, string clientId);
        void RemoveConsent(string subjectId, string clientId);
    }
}