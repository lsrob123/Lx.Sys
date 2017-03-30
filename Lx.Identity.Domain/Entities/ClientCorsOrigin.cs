using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientCorsOrigin : EntityBase, IClientCorsOrigin {
        public Guid ClientKey { get; protected set; }

        [Required]
        [StringLength(150)]
        public string Origin { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}