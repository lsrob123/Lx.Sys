using System;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs
{
    public class UserDto : IUserDto
    {
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto MobileNumber { get; set; }
        public Guid Key { get; set; }
        public VerificationPurpose VerificationPurpose { get; set; }
        public string VerficationCode { get; set; }
        public PriorUserState PriorUserState { get; set; }
        public UserState UserState { get; set; }
        public PersonNameDto Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Nickname { get; set; }
        public AvatarDto Avatar { get; set; }
        public DateTimeOffset? TimeLockedOut { get; set; }
        public UserProfileDto UserProfile { get; set; }
    }
}