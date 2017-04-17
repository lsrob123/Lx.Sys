using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IScopeSecret : IHasScopeKey
    {
        string Description { get; }
        DateTimeOffset? Expiration { get; }
        string Type { get; }
        string Value { get; }
    }
}