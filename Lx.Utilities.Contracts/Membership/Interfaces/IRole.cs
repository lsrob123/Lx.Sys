using System;

namespace Lx.Utilities.Contracts.Membership.Interfaces
{
    public interface IRole
    {
        Guid UserKey { get; }
        string RoleType { get; }
    }
}