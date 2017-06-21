using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Extensions;

namespace Lx.Communications.Services.Porcesses
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public SendEmailResponse SendEmail(SendEmailRequest request)
        {
            var response = _emailSender.SendEmailAsync(request, 0, null).Result.LinkTo(request);
            response.Message = response.Result.Type.Equals(ProcessResultType.Ok)
                ? request.MessageSuccessfulSending
                : request.MessageFailedSending;

            return response;
        }
    }
}