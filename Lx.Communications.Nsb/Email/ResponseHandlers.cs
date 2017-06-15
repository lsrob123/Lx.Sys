using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Services.ServiceBus;
using NServiceBus;

namespace Lx.Communications.Nsb.Email
{
    public class ResponseHandlers : ResponseHandlersBase, IHandleMessages<SendEmailResponse>,
        IHandleMessages<SendEmailProgress>
    {
        public ResponseHandlers(IMediator mediator) : base(mediator)
        {
        }

        public void Handle(SendEmailProgress message)
        {
            Publish(message);
        }

        public void Handle(SendEmailResponse message)
        {
            Publish(message);
        }
    }
}