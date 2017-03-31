using System;
using System.Collections.Generic;
using Lx.Shared.All.Common.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class UpdateUserRequest : RequestBase<UpdateUserResponse>, IBasicUserInfoDto {
        public Guid UserKey { get; set; }
        public string Username { get; set; }
        public PersonNameDto PersonName { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}