using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Infrastructure.RequestDispatching;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Mediator;
using Lx.Utilities.Modelling.DTOs;
using Lx.Utilities.Services.SignalR;

namespace Lx.Utilities.Modelling.Hubs {
    public class TrialHub : MediatedHubBase, IMediatorMessageHandler<TrialResponse> {
        public TrialHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatchingProxy requestDispatchingProxy, IOAuthHelper oauthHelper = null)
            : base(mediator, logger, mappingService, requestDispatchingProxy, oauthHelper) {}

        public void Handle(TrialResponse message) {
            SendGroupResponse(message);
        }

        public void Start(TrialRequest request) {
            Dispatch(request);
        }
    }
}