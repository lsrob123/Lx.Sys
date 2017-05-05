using Lx.Utilities.Contract.Authentication.Enumerations;

namespace Lx.Utilities.Contract.Authentication.Interfaces {
    public interface IRole {
        RoleType RoleType { get; }
    }
}