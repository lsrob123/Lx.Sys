using System;
using System.Collections.Generic;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.RequestsResponses
{
    public class UpdateUserProfileRequest : RequestBase
    {
        public Guid UserKey { get; set; }
        public ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}