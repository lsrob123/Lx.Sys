using Lx.Utilities.Contracts.Email;

namespace Lx.Shared.All.Domains.Communications
{
    public interface IEmailSenderConfig
    {
        EmailParticipant Sender { get; }
    }
}