using Lx.Membership.Contracts.Events;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Services.ServiceBus;
using NServiceBus;

namespace Lx.Membership.Nsb.Members
{
    public class ResponseHandlers : ResponseHandlersBase, IHandleMessages<MemberUpdatedEvent>
    {
        public ResponseHandlers(IMediator mediator) : base(mediator)
        {
        }

        public void Handle(MemberUpdatedEvent message)
        {
            PublishByMediator(message);
        }
    }
}