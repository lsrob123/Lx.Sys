using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientClaim : EntityBase, IClientClaim {
        [Required]
        [StringLength(250)]
        public string Type { get; protected set; }

        [Required]
        [StringLength(250)]
        public string Value { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}