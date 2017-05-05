using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Utilities.Contract.Authentication.Entities {
    public class Role : EntityBase, IRole {
        public RoleType RoleType { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {
            RoleType = RoleType ?? RoleType.Admin;
        }
    }
}