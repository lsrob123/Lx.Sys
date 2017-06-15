using System.Threading.Tasks;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.RequestDispatching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Services.SignalR;

namespace Lx.Communications.SignalR.Hubs
{
    public class EmailHub : MediatedHubBase, IMediatorMessageHandler<SendEmailResponse>,
        IMediatorMessageHandler<SendEmailProgress>
    {
        public EmailHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatchingProxy requestDispatchingProxy, IOAuthHelper oauthHelper = null) : base(mediator, logger,
            mappingService, requestDispatchingProxy, oauthHelper)
        {
        }

        public void Handle(SendEmailProgress message)
        {
            SendGroupResponse(message);
        }

        public void Handle(SendEmailResponse message)
        {
            SendGroupResponse(message);
        }

        public async Task SendEmailAsync(SendEmailRequest request)
        {
            await EnsureInGroupAsync(request);
            RequestDispatchingProxy.Dispatch(request);
        }
    }
}