using System.Threading.Tasks;
using Lx.Membership.Services.APIs;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Extensions;
using Lx.Utilities.Contracts.Infrastructure.RequestDispatching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Services.SignalR;

namespace Lx.Membership.SignalR.Hubs
{
    public class AuthenticationHub : MediatedHubBase
    {
        private readonly IAuthenticationApi _authenticationApi;
        private readonly IOAuthClientService _service;

        public AuthenticationHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatchingProxy requestDispatchingProxy, IOAuthClientService service,
            IAuthenticationApi authenticationApi, IOAuthHelper oauthHelper = null)
            : base(mediator, logger, mappingService, requestDispatchingProxy, oauthHelper)
        {
            _service = service;
            _authenticationApi = authenticationApi;
        }

        public async Task GetTokensAsync(GetTokensRequest request)
        {
            await EnsureInGroupAsync(request, false);
            var response = await _service.GetTokensAsync(request);
            SendGroupResponse(response);

            var getUserInfoRequest = new GetUserInfoRequest
            {
                AccessToken = response.AccessToken
            }.LinkTo(response);

            await GetUserInfoAsync(getUserInfoRequest);
        }

        public async Task RefreshTokensAsync(RefreshTokensRequest request)
        {
            await EnsureInGroupAsync(request);
            var response = await _service.RefreshTokensAsync(request);
            SendGroupResponse(response);
        }

        public async Task GetUserInfoAsync(GetUserInfoRequest request)
        {
            await EnsureInGroupAsync(request);
            var response = await _service.GetUserInfoAsync(request);
            SendGroupResponse(response);
        }

        public async Task RevokeTokenAsync(RevokeTokenRequest request)
        {
            await EnsureInGroupAsync(request);
            var response = await _service.RevokeTokenAsync(request);
            SendGroupResponse(response);
        }

        public async Task ResetPassword(ResetPasswordRequest request)
        {
            await EnsureInGroupAsync(request);
            _authenticationApi.Start(request);
        }
    }
}