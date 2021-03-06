using System;
using System.Collections.Generic;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Shared.All.Domains.Identity.Enumerations;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Persistence.Uow
{
    public interface IUserUowFactory
    {
        UserProfileDto GetUserProfile(Guid userKey, string profileOriginator);
        UserDto GetUser(string usernameOrEmailOrMobileNumber, string userProfileOriginator);
        UserDto GetUser(Guid userKey, string userProfileOriginator);

        (ProcessResult Result, UserDtoBase User, UserUpdateResultType UpdateResultType)
            CreateUser(UserUpdateDto userUpdateDto, ICollection<UserProfileDto> userProfiles);

        UserDto SetVerificationCode(string email, VerificationPurpose verificationPurpose, string hashedVerificationCode, DateTimeOffset timeVerificationCodeExpires);

        ProcessResult ResetPassword(Guid userKey, VerificationPurpose verificationPurpose,
            string plainTextVerificationCode, string newPlainTextPassword);
    }
}