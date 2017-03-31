using System;
using Lx.Shared.All.Common.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class UserKeyEmailDto : IUserKeyEmailDto {
        public Guid Key { get; set; }
        public EmailDto Email { get; set; }
    }
}