﻿using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;

namespace Lx.Utilities.Contract.Membership {
    public class BasicMemberInfo : IMemberInfo {
        public string AvatarUriDefault { get; set; }
        public string AvatarUriRelative { get; set; }
        public Guid Key { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public UserState State { get; set; }
    }
}