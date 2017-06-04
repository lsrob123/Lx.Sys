using Lx.Membership.Contracts.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Membership.Contracts.RequestsResponses
{
    public class CreateMemberRequest : RequestBase
    {
        public MemberUpdateDto Member { get; set; }
    }
}