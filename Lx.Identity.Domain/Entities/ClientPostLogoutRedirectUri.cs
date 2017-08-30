using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities
{
    public class ClientPostLogoutRedirectUri : EntityBase, IClientPostLogoutRedirectUri
    {
        public Guid ClientKey { get; protected set; }

        [StringLength(2000)]
        public string Uri { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
        }
    }
}