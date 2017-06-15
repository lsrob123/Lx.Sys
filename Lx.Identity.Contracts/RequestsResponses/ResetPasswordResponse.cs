using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.RequestsResponses
{
    public class ResetPasswordResponse : ResponseBase
    {
        public string TemporaryPassword { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}