using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Identity.Contracts.DTOs
{
    public class ExecuteVerificationRequest : RequestBase
    {
        public VerificationPurpose VerificationPurpose { get; set; }
        public string VerificationCode { get; set; }
    }
}