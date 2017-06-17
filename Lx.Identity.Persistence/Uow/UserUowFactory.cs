using System;
using System.Collections.Generic;
using Lx.Identity.Domain.Entities;
using Lx.Identity.Persistence.EF;
using Lx.Shared.All.Domains.Identity.Config;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Shared.All.Domains.Identity.Enumerations;
using Lx.Shared.All.Domains.Identity.Events;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.EventBroadcasting;
using Lx.Utilities.Contracts.Infrastructure.Helpers;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Membership.DTOs;
using Lx.Utilities.Contracts.Persistence;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Services.Persistence;

namespace Lx.Identity.Persistence.Uow
{
    public class UserUowFactory : UnitOfWorkFactoryBase<IdentityUow>, IUserUowFactory
    {
        protected readonly IUserProfileConfig UserProfileConfig;

        public UserUowFactory(ILogger logger, ICacheFactory cacheFactory, IMappingService mappingService,
            IDbConfig primaryDbConfig, ISerializer serializer, IEventBroadcastingProxy eventDispatchingProxy,
            IUserProfileConfig userProfileConfig)
            : base(primaryDbConfig, logger, cacheFactory, mappingService, serializer, eventDispatchingProxy)
        {
            UserProfileConfig = userProfileConfig;
        }

        public UserProfileDto GetUserProfile(Guid userKey, string profileOriginator)
        {
            UserProfileDto userProfileDto = null;
            Execute(uow => userProfileDto = GetUserProfile(uow, userKey, profileOriginator));

            return userProfileDto;
        }

        public UserDto GetUser(string usernameOrEmailOrMobileNumber, string userProfileOriginator)
        {
            UserDto userDto = null;
            Execute(uow => { userDto = GetUser(uow, userProfileOriginator, usernameOrEmailOrMobileNumber); });

            return userDto;
        }

        public UserDto GetUser(Guid userKey, string userProfileOriginator)
        {
            UserDto userDto = null;
            Execute(uow =>
            {
                var cacheKey = CacheKeyHelper.GetCacheKey<UserDto>(userKey);
                userDto = uow.Cache.GetCachedItem<UserDto>(cacheKey);
                if (userDto != null)
                {
                    userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
                    return;
                }

                var user = uow.Store.FirstOrDefault<User>(x => x.Key == userKey);
                if (user == null)
                    return;
                userDto = MappingService.Map<UserDto>(user);
                userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
                CacheUserDto(uow, userDto);
            });

            return userDto;
        }

        public (ProcessResult Result, UserDtoBase User, UserUpdateResultType UpdateResultType)
            CreateUser(UserUpdateDto userUpdateDto, ICollection<UserProfileDto> userProfiles)
        {
            var updateResultType = UserUpdateResultType.Unknown;
            UserDto userDto = null;
            var result = ExecuteWithProcessResult(uow =>
            {
                userDto = GetUser(uow, null, userUpdateDto.Email.Address);
                if (userDto != null)
                {
                    updateResultType = UserUpdateResultType.EmailExists;
                    return;
                }

                var user = uow.Store.Add(MappingService.Map<User>(userUpdateDto));
                if (user == null)
                {
                    updateResultType = UserUpdateResultType.GeneralFailure;
                    throw new Exception("Failed to create User.");
                }

                userDto = MappingService.Map<UserDto>(user);
                CacheUserDto(uow, userDto);
                foreach (var userProfileDto in userProfiles)
                {
                    userProfileDto.UserKey = user.Key;
                    var userProfile = MappingService.Map<UserProfile>(userProfileDto);
                    uow.Store.Add(userProfile);
                    var cacheKey = GetUserProfileCacheKey(userProfileDto.UserKey, userProfileDto.UserProfileOriginator);
                    uow.Cache.SetCachedItemAsync(cacheKey, userProfileDto).Wait();
                }
            });

            return (result, userDto, updateResultType);
        }

        private UserDto GetUser(IdentityUow uow, string userProfileOriginator, string usernameOrEmailOrMobileNumber)
        {
            var cacheKey = GetUserCacheKey(usernameOrEmailOrMobileNumber);
            var userDto = uow.Cache.GetCachedItem<UserDto>(cacheKey);
            if (userDto != null)
            {
                userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
                return userDto;
            }

            var localNumberInDigits = usernameOrEmailOrMobileNumber.GetNumberInDigits();
            if (!string.IsNullOrWhiteSpace(localNumberInDigits))
            {
                cacheKey = CacheKeyHelper.GetCacheKey<UserDto>(localNumberInDigits);
                userDto = uow.Cache.GetCachedItem<UserDto>(cacheKey);
            }
            if (userDto != null)
            {
                userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
                return userDto;
            }

            var user = uow.Store.FirstOrDefault<User>(
                x => x.Email.Address == usernameOrEmailOrMobileNumber ||
                     x.Username == usernameOrEmailOrMobileNumber ||
                     x.Mobile.LocalNumberWithAreaCode == usernameOrEmailOrMobileNumber ||
                     x.Mobile.LocalNumberWithAreaCodeInDigits == localNumberInDigits
            );
            if (user == null)
                return null;

            userDto = MappingService.Map<UserDto>(user);
            userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
            CacheUserDto(uow, userDto);
            return userDto;
        }

        private static string GetUserCacheKey(string usernameOrEmailOrMobileNumber)
        {
            return CacheKeyHelper.GetCacheKey<UserDto>(usernameOrEmailOrMobileNumber);
        }

        private UserProfileDto GetUserProfile(IdentityUow uow, Guid userKey, string profileOriginator)
        {
            if (string.IsNullOrWhiteSpace(profileOriginator))
                return null;

            var cacheKey = GetUserProfileCacheKey(userKey, profileOriginator);
            var userProfileDto = uow.Cache.GetCachedItem<UserProfileDto>(cacheKey);
            if (userProfileDto != null)
                return userProfileDto;

            userProfileDto = MappingService.Map<UserProfileDto>(uow.Store.FirstOrDefault<UserProfile>(
                x => x.UserKey == userKey && x.UserProfileOriginator == profileOriginator));
            if (userProfileDto != null)
                uow.Cache.SetCachedItemAsync(cacheKey, userProfileDto).Wait();
            return userProfileDto;
        }

        private static string GetUserProfileCacheKey(Guid userKey, string profileOriginator)
        {
            return CacheKeyHelper.GetCacheKey<UserProfileDto>(profileOriginator + userKey);
        }

        protected override IdentityUow GetUnitOfWork()
        {
            return new IdentityUow(() => new IdentityDbContext(PrimaryDbConfig.ConnectionString), CacheFactory,
                MappingService, Logger);
        }

        /// <summary>
        ///     It doesn't cache user profiles
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="userDto"></param>
        protected void CacheUserDto(IdentityUow uow, UserDto userDto)
        {
            if (userDto == null)
                return;

            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<UserDto>(userDto.Key), userDto);
            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<UserDto>(userDto.Username), userDto);
            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<UserDto>(userDto.Email), userDto);

            uow.Cache.SetCachedItemAsync(
                CacheKeyHelper.GetCacheKey<UserDto>(userDto.Mobile.LocalNumberWithAreaCode), userDto);

            uow.Cache.SetCachedItemAsync(
                CacheKeyHelper.GetCacheKey<UserDto>(userDto.Mobile.LocalNumberWithAreaCodeInDigits), userDto);

            var userUpdatedEvent = new UserUpdatedEvent {UpdatedUser = userDto};
            var userProfile = GetUserProfile(userDto.Key, UserProfileConfig.UserProfileOriginator);
            if (!string.IsNullOrWhiteSpace(userProfile?.Body))
            {
                var basicMemberInfo = Serializer.Deserialize<BasicMemberInfo>(userProfile.Body);
                userUpdatedEvent.Roles = basicMemberInfo.Roles;
            }

            DispatchEvent(userUpdatedEvent);
        }

        protected UserDto UpdateUser(IdentityUow uow, Guid userKey, Action<User> updateAction)
        {
            var user = uow.Store.UpdatePropertiesOnly(x => x.Key == userKey, updateAction);
            var userDto = MappingService.Map<UserDto>(user);
            CacheUserDto(uow, userDto);
            return userDto;
        }

        public ProcessResult SetVerificationCode(string email, VerificationPurpose verificationPurpose,
            string hashedVerificationCode, DateTimeOffset timeVerificationCodeExpires)
        {
            var result = ExecuteWithProcessResult(uow =>
            {
                var user = uow.Store.UpdatePropertiesOnly<User>(x => x.Email.Address == email,
                    x => x.WithVerificationCode(verificationPurpose, hashedVerificationCode,
                        timeVerificationCodeExpires));
                var userDto = MappingService.Map<UserDto>(user);
                CacheUserDto(uow, userDto);

            });

            return result;
        }
    }
}