using System;
using Lx.Identity.Domain.Entities;
using Lx.Identity.Persistence.EF;
using Lx.Shared.All.Identity.Config;
using Lx.Shared.All.Identity.DTOs;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Infrastructure.EventBroadcasting;
using Lx.Utilities.Contract.Infrastructure.Helpers;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Membership;
using Lx.Utilities.Contract.Persistence;
using Lx.Utilities.Contract.Serialization;
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
            Execute(uow =>
            {
                var cacheKey = CacheKeyHelper.GetCacheKey<UserDto>(usernameOrEmailOrMobileNumber);
                userDto = uow.Cache.GetCachedItem<UserDto>(cacheKey);
                if (userDto != null)
                {
                    userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
                    return;
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
                    return;
                }

                var user = uow.Store.FirstOrDefault<User>(
                    x => (x.Email.Address == usernameOrEmailOrMobileNumber) ||
                         (x.Username == usernameOrEmailOrMobileNumber) ||
                         (x.MobileNumber.LocalNumberWithAreaCode == usernameOrEmailOrMobileNumber) ||
                         (x.MobileNumber.LocalNumberWithAreaCodeInDigits == localNumberInDigits)
                    );
                if (user == null)
                    return;

                userDto = MappingService.Map<UserDto>(user);
                userDto.UserProfile = GetUserProfile(uow, userDto.Key, userProfileOriginator);
                CacheUserDto(userDto, uow);
            });

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
                CacheUserDto(userDto, uow);
            });

            return userDto;
        }

        private UserProfileDto GetUserProfile(IdentityUow uow, Guid userKey, string profileOriginator)
        {
            var cacheKey = CacheKeyHelper.GetCacheKey<UserProfileDto>(profileOriginator + userKey);
            var userProfileDto = uow.Cache.GetCachedItem<UserProfileDto>(cacheKey);
            if (userProfileDto != null)
                return userProfileDto;

            userProfileDto = MappingService.Map<UserProfileDto>(uow.Store.FirstOrDefault<UserProfile>(
                x => (x.UserKey == userKey) && (x.UserProfileOriginator == profileOriginator)));
            if (userProfileDto != null)
                uow.Cache.SetCachedItemAsync(cacheKey, userProfileDto).Wait();
            return userProfileDto;
        }

        protected override IdentityUow GetUnitOfWork()
        {
            return new IdentityUow(() => new IdentityDbContext(PrimaryDbConfig.ConnectionString), CacheFactory,
                MappingService, Logger);
        }

        /// <summary>
        ///     It doesn't cache user profiles
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="uow"></param>
        protected void CacheUserDto(UserDto userDto, IdentityUow uow)
        {
            if (userDto == null)
                return;

            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<UserDto>(userDto.Key), userDto);
            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<UserDto>(userDto.Username), userDto);
            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<UserDto>(userDto.Email), userDto);

            uow.Cache.SetCachedItemAsync(
                CacheKeyHelper.GetCacheKey<UserDto>(userDto.MobileNumber.LocalNumberWithAreaCode), userDto);

            uow.Cache.SetCachedItemAsync(
                CacheKeyHelper.GetCacheKey<UserDto>(userDto.MobileNumber.LocalNumberWithAreaCodeInDigits), userDto);

            var userUpdatedEvent = new UserUpdatedEvent {UpdatedUser = userDto};
            var userProfile = GetUserProfile(userDto.Key, UserProfileConfig.UserProfileOriginator);
            if (!string.IsNullOrWhiteSpace(userProfile?.Body))
            {
                var basicMemberInfo = Serializer.Deserialize<BasicMemberInfo>(userProfile.Body);
                userUpdatedEvent.Roles = basicMemberInfo.Roles;
            }

            DispatchEvent(userUpdatedEvent);
        }
    }
}