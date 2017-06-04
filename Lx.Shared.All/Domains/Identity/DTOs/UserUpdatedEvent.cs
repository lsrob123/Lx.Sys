using System.Collections.Generic;
using Lx.Shared.All.Domains.Identity.Enumerations;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Membership.DTOs;

namespace Lx.Shared.All.Domains.Identity.DTOs
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