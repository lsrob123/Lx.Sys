using System.Threading.Tasks;
using Lx.Membership.Contracts.Events;
using Lx.Membership.Contracts.RequestsResponses;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.RequestDispatching;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Mediator;
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