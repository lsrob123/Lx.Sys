using Lx.Membership.Contracts.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Membership.Contracts.Events
{
    public class MemberUpdatedEvent : EventBase
    {
        public string UserProfileOriginator { get; set; }
        public MemberUpdateDto Member { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}