using System;
using Lx.Shared.All.Identity.DTOs;

namespace Lx.Identity.Services.Services {
    public interface IUserService {
        UserDto GetUser(string usernameOrEmailOrMobileNumber);
        UserDto GetUser(Guid userKey);
        UserProfileDto GetUserProfile(string userProfileGroupOriginator, Guid userKey);
    }
}