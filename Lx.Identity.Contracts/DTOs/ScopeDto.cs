using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ScopeDto : IScope
    {
        public Guid Key { get; set; }
        public ICollection<ScopeClaimDto> ScopeClaims { get; set; }
        public ICollection<ScopeSecretDto> ScopeSecrets { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public int Type { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public string ClaimsRule { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public bool AllowUnrestrictedIntrospection { get; set; }
        public DateTimeOffset? TimeCreated { get; set; }
        public DateTimeOffset? TimeModified { get; set; }
    }
}