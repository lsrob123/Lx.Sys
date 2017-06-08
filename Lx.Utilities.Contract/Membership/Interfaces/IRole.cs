using System;
using Lx.Utilities.Contract.Membership.Enumerations;

namespace Lx.Utilities.Contract.Membership.Interfaces {
    public interface IRole {
        Guid UserKey { get; }
        RoleType RoleType { get; }
    }
}