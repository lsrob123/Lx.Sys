using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs {
    public class ScopeClaimDto : IScopeClaim {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }
    }
}