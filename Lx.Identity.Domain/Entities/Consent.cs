using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities
{
    public class Consent : EntityBase, IConsent
    {
        [Required]
        [StringLength(200)]
        public string ClientId { get; protected set; }

        public IEnumerable<string> Scopes { get; protected set; }

        [StringLength(100)]
        public string Subject { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
        }
    }
}