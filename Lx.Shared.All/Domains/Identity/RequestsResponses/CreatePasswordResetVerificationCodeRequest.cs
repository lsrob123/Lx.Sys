using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.RequestsResponses
{
    public class CreatePasswordResetVerificationCodeRequest : RequestBase
    {
        public string Email { get; set; }
    }
}