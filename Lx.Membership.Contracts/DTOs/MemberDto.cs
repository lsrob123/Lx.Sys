using System;
using Lx.Utilities.Contract.Authentication.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Attributes;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Membership.Enumerations;
using Lx.Utilities.Contract.Membership.Interfaces;

namespace Lx.Membership.Contracts.DTOs
{
    public class MemberDto : IDto, IMember<PersonNameDto, EmailDto, PhoneNumberDto, AddressDto>
    {
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