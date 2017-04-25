using System;
using Lx.Shared.All.Common.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;

namespace Lx.Shared.All.Identity.DTOs
{
    public class UserUpdateDto : IUserDtoBase
    {
        public string PlainTextPassword { get; set; }
        public string Username { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto MobileNumber { get; set; }
        public Guid Key { get; set; }
        public UserState UserState { get; set; }
        public PersonNameDto Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Nickname { get; set; }
        public AvatarDto Avatar { get; set; }
    }
}