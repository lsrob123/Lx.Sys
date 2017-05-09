using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Enumerations;

namespace Lx.Utilities.Contract.Membership {
    public interface IMemberInfo {
        Guid Key { get; set; }
        ICollection<RoleDto> Roles { get; set; }
        UserState State { get; set; }
    }
}