using System;

namespace Lx.Utilities.Contract.Membership.Interfaces
{
    public interface IRole
    {
        Guid UserKey { get; }
        string RoleType { get; }
    }
}