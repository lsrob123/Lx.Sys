﻿using System;
using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Enumerations;

namespace Lx.Shared.All.Domains.Identity.DTOs
{
    public class UserDto : UserDtoBase, IUserDto
    {
        public string HashedPassword { get; set; }
        public VerificationPurpose VerificationPurpose { get; set; }
        public string VerficationCode { get; set; }
        public PriorUserState PriorUserState { get; set; }
        public DateTimeOffset? TimeLockedOut { get; set; }
    }
}