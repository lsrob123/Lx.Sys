using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;

namespace Lx.Identity.Services.Services
{
    public interface IScopeService
    {
        IReadOnlyCollection<ScopeDto> ListScopes(IEnumerable<string> scopeNames = null);
    }
}