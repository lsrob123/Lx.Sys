using Lx.Identity.Contracts.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.DTOs
{
    public class ExecuteVerificationResponse : ResponseBase
    {
        public VerificationResult VerificationResult { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}