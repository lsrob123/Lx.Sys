using System.Collections.Generic;
using Lx.Membership.Contracts.Config;
using Lx.Shared.All.Domains.Communications;
using Lx.Shared.All.Domains.Identity.Events;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.Extensions;

namespace Lx.Membership.Services.Processes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmailSenderConfig _emailSenderConfig;
        private readonly IPasswordResetEmailTemplates _passwordResetEmailTemplates;

        public AuthenticationService(IEmailSenderConfig emailSenderConfig,
            IPasswordResetEmailTemplates passwordResetEmailTemplates)
        {
            _emailSenderConfig = emailSenderConfig;
            _passwordResetEmailTemplates = passwordResetEmailTemplates;
        }

        public SendEmailRequest CreateSendEmailRequest(VerificationCodeCreatedEvent verificationCodeCreatedEvent)
        {
            var url = string.Format(_passwordResetEmailTemplates.Url,
                verificationCodeCreatedEvent.PlainTextVerificationCode);

            var sendEmailRequest = new SendEmailRequest
            {
                Sender = _emailSenderConfig.Sender,
                To = new List<EmailParticipant>
                {
                    verificationCodeCreatedEvent.Recipient
                },
                Subject = _passwordResetEmailTemplates.Subject,
                Body = string.Format(_passwordResetEmailTemplates.Body, verificationCodeCreatedEvent.Recipient.Name,
                    url, _emailSenderConfig.Sender.Name),
                MessageSuccessfulSending = "",
                MessageFailedSending = ""
            }.LinkTo(verificationCodeCreatedEvent);

            return sendEmailRequest;
        }
    }
}