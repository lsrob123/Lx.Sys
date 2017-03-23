﻿using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientSecret : EntityBase, IClientSecret {
        [StringLength(2000)]
        public string Value { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}
    }
}