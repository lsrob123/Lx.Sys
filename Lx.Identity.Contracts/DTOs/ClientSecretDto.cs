using System;
using Lx.Identity.Contracts.Interfaces;

namespace Lx.Identity.Contracts.DTOs
{
    public class ClientSecretDto : IClientSecret
    {
        public string Value { get; set; }
        public Guid ClientKey { get; set; }
    }
}