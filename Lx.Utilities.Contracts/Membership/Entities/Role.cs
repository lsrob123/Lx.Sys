using System;
using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contracts.Infrastructure.Domain;
using Lx.Utilities.Contracts.Membership.Interfaces;

namespace Lx.Utilities.Contracts.Membership.Entities
{
    public class Role : EntityBase, IRole
    {
        public Guid UserKey { get; protected set; }

        [StringLength(100)]
        public string RoleType { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
        }
    }
}