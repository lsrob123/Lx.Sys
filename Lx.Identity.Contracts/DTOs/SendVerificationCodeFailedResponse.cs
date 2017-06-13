using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.DTOs
{
    public class SendVerificationCodeFailedResponse : ResponseBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}