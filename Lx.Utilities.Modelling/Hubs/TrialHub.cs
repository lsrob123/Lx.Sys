using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.RequestDispatching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Modelling.DTOs;
using Lx.Utilities.Services.SignalR;

namespace Lx.Utilities.Modelling.Hubs
{
    public class TrialHub : MediatedHubBase, IMediatorMessageHandler<TrialResponse>
    {
        public TrialHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatchingProxy requestDispatchingProxy, IOAuthHelper oauthHelper = null)
            : base(mediator, logger, mappingService, requestDispatchingProxy, oauthHelper)
        {
        }

        public void Handle(TrialResponse message)
        {
            SendGroupResponse(message);
        }

        public void Start(TrialRequest request)
        {
            Dispatch(request);
        }
    }
}