using System;
using Lx.Shared.All.Domains.Identity.DTOs;

namespace Lx.Identity.Services.Processes
{
    public interface IUserService
    {
        UserDto GetUser(string usernameOrEmailOrMobileNumber, string userProfileOriginator);
        UserDto GetUser(Guid userKey, string userProfileOriginator);
        UserProfileDto GetUserProfile(Guid userKeystring, string userProfileOriginator);
        CreateUserResponse CreateUser(CreateUserRequest request);
    }
}