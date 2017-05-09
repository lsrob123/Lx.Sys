using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Utilities.Contract.Authentication.Entities {
    public class RoleProcess : EntityBase, IRoleProcess {
        [StringLength(50)]
        public string Name { get; protected set; }

        [StringLength(200)]
        public string Target { get; protected set; }

        public bool IsDenied { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() { }
    }
}