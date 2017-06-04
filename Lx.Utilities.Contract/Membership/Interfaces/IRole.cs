using Lx.Utilities.Contract.Membership.Enumerations;

namespace Lx.Utilities.Contract.Membership.Interfaces {
    public interface IRole {
        RoleType RoleType { get; }
    }
}