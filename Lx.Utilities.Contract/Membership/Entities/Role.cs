using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Membership.Enumerations;
using Lx.Utilities.Contract.Membership.Interfaces;

namespace Lx.Utilities.Contract.Membership.Entities {
    public class Role : EntityBase, IRole {
        public RoleType RoleType { get; protected set; }

        public override void AssignDefaultValuesToComplexPropertiesIfNull() {
            RoleType = RoleType ?? RoleType.Admin;
        }
    }
}