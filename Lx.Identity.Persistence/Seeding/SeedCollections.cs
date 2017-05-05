using System;
using System.Collections.Generic;
using Lx.Identity.Domain.Entities;
using Lx.Shared.All.Identity.DTOs;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Crypto;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Membership;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Crypto;
using Lx.Utilities.Services.Mapping.AutoMapper;
using Lx.Utilities.Services.Serialization;

namespace Lx.Identity.Persistence.Seeding
{
    public static class SeedCollections
    {
        public static IMappingService MappingService = new MappingService();
        public static ICryptoService CryptoService = new CryptoService();
        public static ISerializer Serializer = new JsonSerializer();

        public static ICollection<User> Users()
        {
            var johnSmith = MappingService.Map<User>(new UserDto
            {
                Key = new Guid("636f1f93-2585-4edc-bbfa-8e5de3778afc"),
                Email = new EmailDto
                {
                    Address = "lsrob@hotmail.com"
                },
                HashedPassword = CryptoService.CreateHash("123"),
                IsAdmin = false,
                MobileNumber = new PhoneNumberDto
                {
                    CountryCode = 61,
                    LocalNumberWithAreaCode = "0421116066",
                    Verified = true
                },
                Name = new PersonNameDto {FamilyName = "Smith", GivenName = "John"},
                Nickname = "john_s",
                UserState = UserState.Active
            });

            return new List<User>
            {
                johnSmith
            };
        }

        public static ICollection<UserProfile> UserProfiles()
        {
            var johnSmith = MappingService.Map<UserProfile>(
                CreateUserProfileDto(new Guid("636f1f93-2585-4edc-bbfa-8e5de3778afc"), "Member"));

            return new List<UserProfile>
            {
                johnSmith
            };
        }

        private static UserProfileDto CreateUserProfileDto(Guid userKey, string roleType) {
            var memberInfo = new BasicMemberInfo {
                Key = userKey,
                State = UserState.Active,
                Roles = new List<RoleDto> {
                    new RoleDto {RoleType = Enumeration.FromName<RoleType>(roleType)}
                }
            };

            var profileJson = Serializer.Serialize(memberInfo);
            return new UserProfileDto
            {
                Key = new Guid("6f1dbc0e-b46d-49ac-b317-11a488121c95"),
                UserKey = userKey,
                Body = profileJson,
                UserProfileOriginator = "default"
            };
        }
    }
}