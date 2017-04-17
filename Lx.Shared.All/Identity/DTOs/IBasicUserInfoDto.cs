using System;
using System.Collections.Generic;
using Lx.Shared.All.Common.DTOs;

namespace Lx.Shared.All.Identity.DTOs
{
    public interface IBasicUserInfoDto
    {
        Guid UserKey { get; set; }
        string Username { get; set; }
        PersonNameDto PersonName { get; set; }
        ICollection<UserProfileDto> UserProfiles { get; set; }
    }
}