using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class Scope : EntityBase, IScope {
        public bool Enabled { get; protected set; }

        [Required]
        [StringLength(200)]
        public string Name { get; protected set; }

        [StringLength(200)]
        public string DisplayName { get; protected set; }

        [StringLength(1000)]
        public string Description { get; protected set; }

        public bool Required { get; protected set; }
        public bool Emphasize { get; protected set; }
        public int Type { get; protected set; }
        public bool IncludeAllClaimsForUser { get; protected set; }

        [StringLength(200)]
        public string ClaimsRule { get; protected set; }

        public bool ShowInDiscoveryDocument { get; protected set; }
        public bool AllowUnrestrictedIntrospection { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}