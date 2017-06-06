using Lx.Membership.Contracts.Events;
using Lx.Membership.Contracts.RequestsResponses;

namespace Lx.Membership.Services.Processes
{
    public interface IMemberService
    {
        MemberUpdatedEvent CreateOrUpdateMember(UpdateMemberRequestBase request);
    }
}