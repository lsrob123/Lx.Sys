using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.Attributes;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.RequestsResponses
{
    public class ResetPasswordRequest : RequestBase
    {
        public string Email { get; set; }
    }
}