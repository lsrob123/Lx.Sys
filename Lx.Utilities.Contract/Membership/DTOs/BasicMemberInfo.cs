using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Membership.Enumerations;

namespace Lx.Utilities.Contract.Membership.DTOs
{
    public class BasicMemberInfo : IMemberInfo
    {
        public string AvatarUriDefault { get; set; }
        public string AvatarUriRelative { get; set; }
        public string Nickname { get; set; }
        public Guid Key { get; set; }
        public AddressDto HomeAddress { get; set; }
        public AddressDto WorkAddress { get; set; }
        public AddressDto PostalAddress { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public UserState UserState { get; set; }
        public PersonNameDto PersonName { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public AccountState AccountState { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto Mobile { get; set; }
    }
}