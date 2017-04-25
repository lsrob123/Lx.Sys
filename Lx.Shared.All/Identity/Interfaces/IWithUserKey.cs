using System;

namespace Lx.Shared.All.Identity.Interfaces
{
    public interface IWithUserKey
    {
        Guid UserKey { get; }
    }
}