using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IScopeSecret : IHasScopeKey
    {
        Guid Key { get; }
        string Description { get; }
        DateTimeOffset? Expiration { get; }
        string Type { get; }
        string Value { get; }
    }
}