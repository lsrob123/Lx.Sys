using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Membership.Domain.Entities
{
    public class RoleProcess : EntityBase, IRoleProcess
    {
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Target { get; set; }

        public bool IsDenied { get; set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull()
        {
        }
    }
}