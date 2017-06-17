using Lx.Shared.All.Domains.Identity.Config;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Contracts.ServiceBus;
using Lx.Utilities.Services.Infrastructure;
using NServiceBus;

namespace Lx.Membership.Services.APIs
{
    public class AuthenticationApi : MediatedApiBase, IAuthenticationApi
    {
        private readonly ICommonBusEndpointSettings _commonBusEndpointSettings;

        public AuthenticationApi(IMediator mediator, IBus bus, IBusSettings busSettings, ISerializer serializer,
            ICommonBusEndpointSettings commonBusEndpointSettings) : base(mediator, bus, busSettings, serializer)
        {
            _commonBusEndpointSettings = commonBusEndpointSettings;
        }

        public void Start(ResetPasswordRequest request)
        {
            SendToBus(request, _commonBusEndpointSettings.Identity);
        }
    }
}