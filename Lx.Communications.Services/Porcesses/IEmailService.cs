using Lx.Utilities.Contracts.Email;

namespace Lx.Communications.Services.Porcesses
{
    public interface IEmailService
    {
        SendEmailResponse SendEmail(SendEmailRequest request);
    }
}