using System.Collections.Generic;
using Lx.Identity.Contracts.DTOs;

namespace Lx.Identity.Services.Processes
{
    public interface IScopeService
    {
        ICollection<ScopeDto> ListScopes(IEnumerable<string> scopeNames);
    }
}