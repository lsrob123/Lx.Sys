using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ScopeSecret : EntityBase, IScopeSecret {
        public Guid ScopeKey { get; protected set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public DateTimeOffset? Expiration { get; set; }

        [StringLength(250)]
        public string Type { get; set; }

        [Required]
        [StringLength(250)]
        public string Value { get; set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}