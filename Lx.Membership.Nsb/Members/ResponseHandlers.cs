using Lx.Membership.Contracts.Events;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Services.ServiceBus;
using NServiceBus;

namespace Lx.Membership.Nsb.Members
{
    public class ResponseHandlers : ResponseHandlersBase, IHandleMessages<MemberUpdatedEvent>,
        IHandleMessages<ResetPasswordResponse>
    {
        public ResponseHandlers(IMediator mediator) : base(mediator)
        {
        }

        public void Handle(MemberUpdatedEvent message)
        {
            PublishByMediator(message);
        }

        public void Handle(ResetPasswordResponse message)
        {
            PublishByMediator(message);
        }
    }
}