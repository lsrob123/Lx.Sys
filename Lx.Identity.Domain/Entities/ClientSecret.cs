using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities {
    public class ClientSecret : EntityBase, IClientSecret {
        public Guid ClientKey { get; protected set; }

        [StringLength(2000)]
        public string Value { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {}

        public ClientSecret WithClient(IClient client)
        {
            ClientKey = client.Key;
            return this;
        }
    }
}