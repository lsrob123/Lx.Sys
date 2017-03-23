using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class Scope : EntityBase {
        public bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string DisplayName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public int Type { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }

        [StringLength(200)]
        public string ClaimsRule { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
        public bool AllowUnrestrictedIntrospection { get; set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}