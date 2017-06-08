using Lx.Membership.Contracts.DTOs;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Membership.Contracts.RequestsResponses
{
    public class UpdateMemberRequestBase : RequestBase
    {
        public MemberUpdateDto Member { get; set; }
    }
}