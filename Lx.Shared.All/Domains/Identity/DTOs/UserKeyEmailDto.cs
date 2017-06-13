using System;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class UserKeyEmailDto : IUserKeyEmailDto
    {
        public Guid Key { get; set; }
        public EmailDto Email { get; set; }
    }
}