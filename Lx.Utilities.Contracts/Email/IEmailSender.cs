using System.Collections.Generic;
using System.Threading.Tasks;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Email
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailRequest request, int interval,
            IProgressReporter<SendEmailProgress> progressReporter);

        Task<SendEmailResponse> SendEmailAsync(EmailParticipant sender, IEnumerable<EmailParticipant> to,
            IEnumerable<EmailParticipant> cc, IEnumerable<EmailParticipant> bcc, int interval, string subject,
            string content, bool isHtml, IEnumerable<EmailAttachment> attachments,
            IProgressReporter<SendEmailProgress> progressReporter);
    }
}