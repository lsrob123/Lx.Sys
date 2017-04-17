using System;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ClientScopeDto : IClientScope
    {
        public string Scope { get; set; }
        public Guid ClientKey { get; set; }
    }
}