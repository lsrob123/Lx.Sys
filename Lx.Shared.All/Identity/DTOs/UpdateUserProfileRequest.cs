using System;
using System.Collections.Generic;

namespace Lx.Shared.All.Identity.DTOs {
    public class UpdateUserProfileRequest : RequestBase<UpdateUserProfileResponse> {
        public Guid UserKey { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}