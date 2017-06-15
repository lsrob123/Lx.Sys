using Lx.Communications.Services.Porcesses;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Communications.Nsb.Email
{
    public class RequestHandlers : RequestHandlersBase, IHandleMessages<SendEmailRequest>
    {
        private readonly IEmailService _emailService;

        public RequestHandlers(IBus bus, IEmailService emailService) : base(bus)
        {
            _emailService = emailService;
        }

        public void Handle(SendEmailRequest message)
        {
            var response = _emailService.SendEmail(message);
            PublishToBus(response);
        }
    }
}