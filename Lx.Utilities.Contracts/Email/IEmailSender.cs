using System.Collections.Generic;
using System.Threading.Tasks;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Email
{
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(IDictionary<string, string> to, IDictionary<string, string> cc,
            IDictionary<string, string> bcc, string name, string from, int interval, string subject, string content,
            bool isHtml, IEnumerable<IEmailAttachment> attachments,
            IProgressReporter<SendEmailProgress> progressReporter);
    }
}