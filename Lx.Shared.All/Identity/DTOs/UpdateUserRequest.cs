using System;
using System.Collections.Generic;
using Lx.Shared.All.Common.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class UpdateUserRequest : RequestBase, IBasicUserInfoDto {
        public Guid UserKey { get; set; }
        public string Username { get; set; }
        public PersonNameDto PersonName { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}