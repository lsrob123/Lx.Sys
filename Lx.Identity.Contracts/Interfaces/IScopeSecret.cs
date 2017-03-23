using System;

namespace Lx.Identity.Contracts.Interfaces {
    public interface IScopeSecret {
        string Description { get; }
        DateTimeOffset? Expiration { get; }
        string Type { get; }
        string Value { get; }
    }
}