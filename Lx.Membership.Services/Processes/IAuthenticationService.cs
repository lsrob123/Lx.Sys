using Lx.Shared.All.Domains.Identity.Events;
using Lx.Utilities.Contracts.Email;

namespace Lx.Membership.Services.Processes
{
    public interface IAuthenticationService
    {
        SendEmailRequest CreateSendEmailRequest(VerificationCodeCreatedEvent verificationCodeCreatedEvent);
    }
}