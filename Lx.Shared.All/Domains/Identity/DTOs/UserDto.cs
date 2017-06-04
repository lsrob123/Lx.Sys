using System;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class UserDto : IUserDto
    {
        public string HashedPassword { get; set; }
        public VerificationPurpose VerificationPurpose { get; set; }
        public string VerficationCode { get; set; }
        public PriorUserState PriorUserState { get; set; }
        public string Nickname { get; set; }
        public DateTimeOffset? TimeLockedOut { get; set; }
        public UserProfileDto UserProfile { get; set; }
        public string Username { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto Mobile { get; set; }
        public Guid Key { get; set; }
        public AddressDto HomeAddress { get; set; }
        public AddressDto WorkAddress { get; set; }
        public AddressDto PostalAddress { get; set; }
        public UserState UserState { get; set; }
        public PersonNameDto PersonName { get; set; }
        public bool IsAdmin { get; set; }
    }
}