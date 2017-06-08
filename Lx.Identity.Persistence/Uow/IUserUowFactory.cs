using System;
using Lx.Shared.All.Domains.Identity.DTOs;

namespace Lx.Identity.Persistence.Uow
{
    public interface IUserUowFactory
    {
        UserProfileDto GetUserProfile(Guid userKey, string profileOriginator);
        UserDto GetUser(string usernameOrEmailOrMobileNumber, string userProfileOriginator);
        UserDto GetUser(Guid userKey, string userProfileOriginator);
    }
}