using System;
using Lx.Shared.All.Identity.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Shared.All.Identity.DTOs {
    public class UserProfileDto : IUserProfile, IDto {
        public string UserProfileOriginator { get; set; }
        public Guid Key { get; set; }
        public string Body { get; set; }
        public Guid UserKey { get; set; }
    }
}