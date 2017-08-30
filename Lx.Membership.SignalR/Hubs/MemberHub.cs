using System.Threading.Tasks;
using Lx.Membership.Contracts.Events;
using Lx.Membership.Contracts.RequestsResponses;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.RequestDispatching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Services.SignalR;

namespace Lx.Membership.SignalR.Hubs
{
    public class MemberHub : MediatedHubBase, IMediatorMessageHandler<MemberUpdatedEvent>
    {
        public MemberHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatchingProxy requestDispatchingProxy, IOAuthHelper oauthHelper = null) : base(mediator, logger,
            mappingService, requestDispatchingProxy, oauthHelper)
        {
        }

        public void Handle(MemberUpdatedEvent message)
        {
            SendGroupResponse(message);
        }

        public async Task CreateMemberAsync(CreateMemberRequest request)
        {
            await EnsureInGroupAsync(request);
            RequestDispatchingProxy.Dispatch(request);
        }
    }
}