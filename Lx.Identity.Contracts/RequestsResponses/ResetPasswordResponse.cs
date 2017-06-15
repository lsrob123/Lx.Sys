using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.RequestsResponses
{
    public class ResetPasswordResponse : ResponseBase
    {
        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}