using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.RequestsResponses
{
    public class SendVerificationCodeFailedResponse : ResponseBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}