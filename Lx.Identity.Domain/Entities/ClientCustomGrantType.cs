using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientCustomGrantType : EntityBase, IClientCustomGrantType {
        [Required]
        [StringLength(250)]
        public virtual string GrantType { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}