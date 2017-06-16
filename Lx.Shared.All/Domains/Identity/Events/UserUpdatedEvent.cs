using System.Collections.Generic;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Shared.All.Domains.Identity.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Membership.DTOs;

namespace Lx.Shared.All.Domains.Identity.Events
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