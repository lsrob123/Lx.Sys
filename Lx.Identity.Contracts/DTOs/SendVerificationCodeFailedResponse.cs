using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.DTOs
{
    public class SendVerificationCodeFailedResponse : ResponseBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}