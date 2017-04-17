using System;
using Lx.Identity.Domain.Entities;

namespace Lx.Identity.Persistence.Uow
{
    public interface IUserUowFactory
    {
        UserProfile GetUserProfile(Guid userKey, string profileOriginator);
        User GetUser(string usernameOrEmailOrMobileNumber);
        User GetUser(Guid userKey);
    }
}