using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Modelling.DTOs;
using Lx.Utilities.Services.SignalR;

namespace Lx.Utilities.Modelling.Hubs {
    public class TrialHub : MediatedHubBase, IMediatorMessageHandler<TrialResponse> {
        public TrialHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatcher requestDispatcher, IOAuthHelper oauthHelper = null)
            : base(mediator, logger, mappingService, requestDispatcher, oauthHelper) {}

        public void Handle(TrialResponse message) {
            SendGroupResponse(message);
        }

        public void Start(TrialRequest request) {
            Dispatch(request);
        }
    }
}