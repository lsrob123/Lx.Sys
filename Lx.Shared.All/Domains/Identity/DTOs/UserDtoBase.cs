﻿using System;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class UserDtoBase
    {
        public string Nickname { get; set; }
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
        public UserProfileDto UserProfile { get; set; }
    }
}