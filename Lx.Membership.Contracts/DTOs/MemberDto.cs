using System;
using System.Collections.Generic;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Attributes;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Membership.DTOs;
using Lx.Utilities.Contracts.Membership.Enumerations;
using Lx.Utilities.Contracts.Membership.Interfaces;

namespace Lx.Membership.Contracts.DTOs
{
    public class MemberDto : IDto, IMember<PersonNameDto, EmailDto, PhoneNumberDto, AddressDto>
    {
        [InvisibleInTestExample]
        public ICollection<RoleDto> Roles { get; set; }

        public Guid Key { get; set; }
        public AddressDto HomeAddress { get; set; }
        public AddressDto WorkAddress { get; set; }
        public AddressDto PostalAddress { get; set; }

        [InvisibleInTestExample]
        public UserState UserState { get; set; }

        public PersonNameDto PersonName { get; set; }
        public bool IsAdmin { get; set; }
        public string Username { get; set; }

        [InvisibleInTestExample]
        public AccountState AccountState { get; set; }

        [InvisibleInTestExample]
        public string AvatarUriDefault { get; set; }

        [InvisibleInTestExample]
        public string AvatarUriRelative { get; set; }

        public string Nickname { get; set; }

        public EmailDto Email { get; set; }
        public PhoneNumberDto Mobile { get; set; }
    }
}