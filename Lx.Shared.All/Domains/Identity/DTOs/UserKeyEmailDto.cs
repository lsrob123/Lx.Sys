using System;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs {
    public class UserKeyEmailDto : IUserKeyEmailDto {
        public Guid Key { get; set; }
        public EmailDto Email { get; set; }
    }
}