using System;
using System.ComponentModel.DataAnnotations;
using Lx.Identity.Contracts.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Identity.Domain.Entities
{
    public class ScopeClaim : EntityBase, IScopeClaim
    {
        public Guid ScopeKey { get; protected set; }

        [Required]
        [StringLength(200)]
        public string Name { get; protected set; }

        [StringLength(1000)]
        public string Description { get; protected set; }

        public bool AlwaysIncludeInIdToken { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
        }

        public ScopeClaim WithScope(IScope scope)
        {
            ScopeKey = scope.Key;
            return this;
        }
    }
}