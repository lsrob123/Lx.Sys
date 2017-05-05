using System;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ScopeClaimDto : IScopeClaim
    {
        public Guid Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }
        public Guid ScopeKey { get; set; }
    }
}