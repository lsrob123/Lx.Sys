using System;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ClientRedirectUriDto : IClientRedirectUri
    {
        public string Uri { get; set; }
        public Guid ClientKey { get; set; }
    }
}