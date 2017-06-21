using System.Collections.Generic;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Email
{
    public class SendEmailRequest : RequestBase
    {
        public EmailParticipant Sender { get; set; }
        public ICollection<EmailParticipant> To { get; set; }
        public ICollection<EmailParticipant> Cc { get; set; }
        public ICollection<EmailParticipant> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public ICollection<EmailAttachment> Attachments { get; set; }
        public string MessageSuccessfulSending { get; set; }
        public string MessageFailedSending { get; set; }
    }
}