using System;
using Lx.Identity.Persistence.Uow;
using Lx.Shared.All.Identity.DTOs;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Identity.Services.Services
{
    public class UserService : IUserService
    {
        protected readonly IMappingService MappingService;
        protected readonly IUserUowFactory UserUowFactory;

        public UserService(IUserUowFactory userUowFactory, IMappingService mappingService)
        {
            UserUowFactory = userUowFactory;
            MappingService = mappingService;
        }

        public UserDto GetUser(string usernameOrEmailOrMobileNumber, string userProfileOriginator)
        {
            var userDto = UserUowFactory.GetUser(usernameOrEmailOrMobileNumber, userProfileOriginator);
            return userDto;
        }

        public UserDto GetUser(Guid userKey, string userProfileOriginator)
        {
            var userDto = UserUowFactory.GetUser(userKey, userProfileOriginator);
            return userDto;
        }

        public UserProfileDto GetUserProfile(Guid userKeystring, string userProfileOriginator)
        {
            var userProfileDto = UserUowFactory.GetUserProfile(userKeystring, userProfileOriginator);
            return userProfileDto;
        }
    }
}