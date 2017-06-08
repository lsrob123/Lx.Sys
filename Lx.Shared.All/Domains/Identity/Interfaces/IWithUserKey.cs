using System;

namespace Lx.Shared.All.Domains.Identity.Interfaces
{
    public interface IWithUserKey
    {
        Guid UserKey { get; }
    }
}