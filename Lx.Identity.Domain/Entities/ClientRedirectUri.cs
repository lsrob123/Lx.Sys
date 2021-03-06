﻿using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities
{
    public class ClientRedirectUri : EntityBase, IClientRedirectUri
    {
        public Guid ClientKey { get; protected set; }

        [Required]
        [StringLength(2000)]
        public string Uri { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
        }

        public ClientRedirectUri WithClient(IClient client)
        {
            ClientKey = client.Key;
            return this;
        }
    }
}