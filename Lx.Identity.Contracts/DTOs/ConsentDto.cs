using System;
using System.Collections.Generic;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ConsentDto : IConsent
    {
        public string ClientId { get; set; }
        public IEnumerable<string> Scopes { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset? TimeCreated { get; set; }
        public DateTimeOffset? TimeModified { get; set; }
        public Guid Key { get; }
    }
}