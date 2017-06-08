using System;
using System.Collections.Generic;
using Lx.Identity.Domain.Entities;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Crypto;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Membership.Constants;
using Lx.Utilities.Contract.Membership.DTOs;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Crypto;
using Lx.Utilities.Services.Mapping.AutoMapper;
using Lx.Utilities.Services.Serialization;

namespace Lx.Identity.Persistence.Seeding
{
    public static class UserSeedCollections
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
                Mobile = new PhoneNumberDto
                {
                    CountryCode = 61,
                    LocalNumberWithAreaCode = "0421116066",
                    Verified = true
                },
                PersonName = new PersonNameDto {FamilyName = "Smith", GivenName = "John"},
                Username = "john",
                Nickname = "john_s",
                UserState = UserState.Active
            });

            var mariaGarcia = MappingService.Map<User>(new UserDto
            {
                Key = new Guid("5a9731cd-3e38-478a-befa-73ab6b45b07e"),
                Email = new EmailDto
                {
                    Address = "lsrob.g@gmail.com"
                },
                HashedPassword = CryptoService.CreateHash("123"),
                IsAdmin = false,
                Mobile = new PhoneNumberDto
                {
                    CountryCode = 61,
                    LocalNumberWithAreaCode = "0433058893",
                    Verified = true
                },
                PersonName = new PersonNameDto {FamilyName = "Garcia", GivenName = "Maria"},
                Username = "maria",
                Nickname = "maria",
                UserState = UserState.Active
            });

            return new List<User>
            {
                johnSmith,
                mariaGarcia
            };
        }

        public static ICollection<UserProfile> UserProfiles()
        {
            var johnSmith = MappingService.Map<UserProfile>(
                CreateUserProfileDto(new Guid("520715a7-67f7-4f8e-9bb7-9bed1f59b5bc"),
                    new Guid("636f1f93-2585-4edc-bbfa-8e5de3778afc"), RoleTypeName.Admin));

            var mariaGarcia = MappingService.Map<UserProfile>(
                CreateUserProfileDto(new Guid("213d2b63-986c-4883-bc6e-c0478bfae3d9"),
                    new Guid("5a9731cd-3e38-478a-befa-73ab6b45b07e"), RoleTypeName.BasicMember));

            return new List<UserProfile>
            {
                johnSmith,
                mariaGarcia
            };
        }

        private static UserProfileDto CreateUserProfileDto(Guid key, Guid userKey, string roleType)
        {
            var memberInfo = new BasicMemberInfo
            {
                Key = userKey,
                UserState = UserState.Active,
                Roles = new List<RoleDto>
                {
                    new RoleDto {RoleType = roleType}
                }
            };

            var profileJson = Serializer.Serialize(memberInfo);
            return new UserProfileDto
            {
                Key = key,
                UserKey = userKey,
                Body = profileJson,
                UserProfileOriginator = "default"
            };
        }
    }
}