using System;
using Lx.Shared.All.Domains.Identity.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class UserProfileDto : IUserProfile, IDto
    {
        public string UserProfileOriginator { get; set; }
        public Guid Key { get; set; }
        public string Body { get; set; }
        public Guid UserKey { get; set; }
    }
}