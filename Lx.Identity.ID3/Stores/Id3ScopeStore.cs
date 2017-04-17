using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Lx.Identity.Services.Services;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Identity.ID3.Stores
{
    public class Id3ScopeStore : IScopeStore
    {
        protected readonly IMappingService MappingService;
        protected readonly IScopeService ScopeService;

        public Id3ScopeStore(IScopeService scopeService, IMappingService mappingService)
        {
            ScopeService = scopeService;
            MappingService = mappingService;
        }

        public async Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            var scopes = await Task.Run(() => ScopeService
                .ListScopes(scopeNames)
                .Select(x => MappingService.Map<Scope>(x)));
            return scopes;
        }

        public async Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            var scopes = await Task.Run(() => ScopeService.ListScopes().Select(x => MappingService.Map<Scope>(x)));
            return scopes;
        }
    }
}