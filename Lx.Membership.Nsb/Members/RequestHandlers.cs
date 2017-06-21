using Lx.Membership.Services.Processes;
using Lx.Shared.All.Domains.Identity.Config;
using Lx.Shared.All.Domains.Identity.Events;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Membership.Nsb.Members
{
    public class RequestHandlers : RequestHandlersBase, IHandleMessages<VerificationCodeCreatedEvent>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICommonBusEndpointSettings _commonBusEndpointSettings;

        public RequestHandlers(IBus bus, ICommonBusEndpointSettings commonBusEndpointSettings,
            IAuthenticationService authenticationService) : base(bus)
        {
            _commonBusEndpointSettings = commonBusEndpointSettings;
            _authenticationService = authenticationService;
        }

        public void Handle(VerificationCodeCreatedEvent message)
        {
            var request = _authenticationService.CreateSendEmailRequest(message);
            SendToBusEndpoint(request, _commonBusEndpointSettings.Communications);
        }
    }
}