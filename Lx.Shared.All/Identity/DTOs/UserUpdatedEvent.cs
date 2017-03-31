using System.Collections.Generic;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs {
    public class UserUpdatedEvent : ResponseBase {
        public UserDto UpdatedUser { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public override void EraseShareGroupInfoForClientSide() {}
    }
}