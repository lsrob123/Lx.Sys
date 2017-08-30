using System;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IScope
    {
        bool Enabled { get; }
        string Name { get; }
        string DisplayName { get; }
        string Description { get; }
        bool Required { get; }
        bool Emphasize { get; }
        int Type { get; }
        bool IncludeAllClaimsForUser { get; }
        string ClaimsRule { get; }
        bool ShowInDiscoveryDocument { get; }
        bool AllowUnrestrictedIntrospection { get; }
        DateTimeOffset? TimeCreated { get; }
        DateTimeOffset? TimeModified { get; }
        Guid Key { get; }
    }
}