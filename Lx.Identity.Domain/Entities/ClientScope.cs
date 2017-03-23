﻿using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientScope : EntityBase, IClientScope {
        [Required]
        [StringLength(200)]
        public string Scope { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}