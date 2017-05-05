using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IScopeClaim : IHasScopeKey
    {
        Guid Key { get; }
        string Name { get; }
        string Description { get; }
        bool AlwaysIncludeInIdToken { get; }
    }
}