using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs {
    public class UpdateUserProfileRequest : RequestBase {
        public Guid UserKey { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}