﻿using System;
using System.Collections.Generic;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Membership.Enumerations;

namespace Lx.Utilities.Contracts.Membership.DTOs
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
        public MemberState MemberState { get; set; }
        public EmailDto Email { get; set; }
        public PhoneNumberDto Mobile { get; set; }
    }
}