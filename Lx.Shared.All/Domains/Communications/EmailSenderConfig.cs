using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Services.Config;

namespace Lx.Shared.All.Domains.Communications
{
    public class EmailSenderConfig : IEmailSenderConfig
    {
        protected string SenderName => this.AppSettingStringValue(x => x.SenderName);
        protected string SenderEmail => this.AppSettingStringValue(x => x.SenderEmail);

        public EmailParticipant Sender => new EmailParticipant {EmailAddress = SenderEmail, Name = SenderName};
    }
}