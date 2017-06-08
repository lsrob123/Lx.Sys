using System;
using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Membership.Interfaces;

namespace Lx.Utilities.Contract.Membership.Entities
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