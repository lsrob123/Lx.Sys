using Lx.Membership.Contracts.DTOs;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Membership.Contracts.Events
{
    public class MemberUpdatedEvent : EventBase
    {
        public string UserProfileOriginator { get; set; }
        public MemberUpdateDto Member { get; set; }
        public UserProfileDto UserProfile { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }

        public MemberUpdatedEvent WithMember(MemberUpdateDto member)
        {
            Member = member;
            return this;
        }

        public MemberUpdatedEvent WithUserProfile(UserProfileDto userProfile)
        {
            UserProfile = userProfile;
            return this;
        }
    }
}