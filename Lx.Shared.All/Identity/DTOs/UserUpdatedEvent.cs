using System.Collections.Generic;
using Lx.Shared.All.Identity.Enumerations;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Shared.All.Identity.DTOs
{
    public class UserUpdatedEvent : EventBase
    {
        public UserDto UpdatedUser { get; set; }
        public ICollection<RoleDto> Roles { get; set; }
        public UserUpdateResultType UpdateResultType { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}