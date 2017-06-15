using System;
using Lx.Identity.Persistence.Uow;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Extensions;
using Lx.Utilities.Contracts.Mapping;

namespace Lx.Identity.Services.Processes
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

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var updateResult = UserUowFactory.CreateUser(request.UserUpdate, request.UserProfiles);
            var response = new CreateUserResponse
            {
                User = updateResult.User,
                ResultType = updateResult.UpdateResultType
            }.WithProcessResult(updateResult.Result).LinkTo(request);
            return response;
        }
    }
}