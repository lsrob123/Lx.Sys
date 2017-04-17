using System;
using System.Collections.Generic;

namespace Lx.Identity.Contracts.Interfaces
{
    public interface IConsent
    {
        string ClientId { get; }
        IEnumerable<string> Scopes { get; }
        string Subject { get; }
        DateTimeOffset? TimeCreated { get; }
        DateTimeOffset? TimeModified { get; }
        Guid Key { get; }
    }
}