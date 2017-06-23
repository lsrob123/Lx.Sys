using System;
using Lx.Shared.All.Domains.Identity.Events;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Email;

namespace Lx.Membership.Services.Processes
{
    public interface IAuthenticationService
    {
        SendEmailRequest CreateSendEmailRequest(VerificationCodeCreatedEvent verificationCodeCreatedEvent);
    }
}