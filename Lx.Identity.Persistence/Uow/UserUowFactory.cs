using System;
using Lx.Identity.Domain.Entities;
using Lx.Identity.Persistence.EF;
using Lx.Shared.All.Identity.Constants;
using Lx.Shared.All.Identity.DTOs;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Infrastructure.EventDispacthing;
using Lx.Utilities.Contract.Infrastructure.Helpers;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Membership;
using Lx.Utilities.Contract.Persistence;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Persistence;

namespace Lx.Identity.Persistence.Uow {
    public class UserUowFactory : UnitOfWorkFactoryBase<IdentityUow> {
        public UserUowFactory(ILogger logger, ICacheFactory cacheFactory, IMappingService mappingService,
            IDbConfig primaryDbConfig, ISerializer serializer, IEventDispatchingProxy eventDispatchingProxy)
            : base(primaryDbConfig, logger, cacheFactory, mappingService, serializer, eventDispatchingProxy) {}

        protected override IdentityUow GetUnitOfWork() {
            return new IdentityUow(() => new IdentityDbContext(PrimaryDbConfig.ConnectionString), CacheFactory,
                MappingService, Logger);
        }

        public UserProfile GetUserProfile(Guid userKey, string profileOriginator) {
            UserProfile userProfile = null;
            Execute(uow => {
                var cacheKey = GetUserProfileCacehKey(profileOriginator, userKey);
                userProfile = uow.Cache.GetCachedItem<UserProfile>(cacheKey) ??
                              uow.Store.FirstOrDefault<UserProfile>(
                                  x => (x.UserKey == userKey) && (x.UserProfileOriginator == profileOriginator));
            });

            return userProfile;
        }

        private static void CacheUserProfile(IdentityUow uow, UserProfile userProfileEntity) {
            var cacheKey = GetUserProfileCacehKey(userProfileEntity.UserProfileOriginator, userProfileEntity.UserKey);
            uow.Cache.SetCachedItemAsync(cacheKey, userProfileEntity).Wait();
        }

        private static string GetUserProfileCacehKey(string profileOriginator, Guid userKey) {
            var key = CacheKeyHelper.GetCacheKey<UserProfile>(profileOriginator + userKey);
            return key;
        }

        public User GetUser(string usernameOrEmailOrMobileNumber) {
            User user = null;
            Execute(uow => {
                var cacheKey = CacheKeyHelper.GetCacheKey<User>(usernameOrEmailOrMobileNumber);
                user = uow.Cache.GetCachedItem<User>(cacheKey);
                if (user != null)
                    return;

                var localNumberInDigits = PhoneNumberHelper.GetNumberInDigits(usernameOrEmailOrMobileNumber);
                if (!string.IsNullOrWhiteSpace(localNumberInDigits)) {
                    cacheKey = CacheKeyHelper.GetCacheKey<User>(localNumberInDigits);
                    user = uow.Cache.GetCachedItem<User>(cacheKey);
                }
                if (user != null)
                    return;

                user = uow.Store.FirstOrDefault<User>(
                    x => (x.Email.Address == usernameOrEmailOrMobileNumber) ||
                         (x.Username == usernameOrEmailOrMobileNumber) ||
                         (x.MobileNumber.LocalNumberWithAreaCode == usernameOrEmailOrMobileNumber) ||
                         (x.MobileNumber.LocalNumberWithAreaCodeInDigits == localNumberInDigits)
                );

                CacheUser(user, uow);
            });

            return user;
        }

        public User GetUser(Guid userKey) {
            User user = null;
            Execute(uow => {
                var cacheKey = CacheKeyHelper.GetCacheKey<User>(userKey);
                user = uow.Cache.GetCachedItem<User>(cacheKey) ??
                       uow.Store.FirstOrDefault<User>(x => x.Key == userKey);
            });

            return user;
        }

        protected void CacheUser(User user, IdentityUow uow) {
            if (user == null)
                return;

            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(user.Key), user);
            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(user.Username), user);
            uow.Cache.SetCachedItemAsync(CacheKeyHelper.GetCacheKey<User>(user.Email), user);

            uow.Cache.SetCachedItemAsync(
                CacheKeyHelper.GetCacheKey<User>(user.MobileNumber.LocalNumberWithAreaCode), user);

            uow.Cache.SetCachedItemAsync(
                CacheKeyHelper.GetCacheKey<User>(user.MobileNumber.LocalNumberWithAreaCodeInDigits), user);

            var userUpdatedEvent = new UserUpdatedEvent {UpdatedUser = MappingService.Map<UserDto>(user)};
            var userProfile = GetUserProfile(user.Key, UserProfileOriginator.Default);
            if (!string.IsNullOrWhiteSpace(userProfile?.Body)) {
                var basicMemberInfo = Serializer.Deserialize<BasicMemberInfo>(userProfile.Body);
                userUpdatedEvent.Roles = basicMemberInfo.Roles;
            }

            EventDispatchingProxy.Dispatch(userUpdatedEvent);
        }
    }
}