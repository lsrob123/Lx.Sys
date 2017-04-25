﻿using System.Threading.Tasks;
using Lx.Utilities.Contract.Authentication;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.RequestDispatching;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Mediator;
using Lx.Utilities.Services.SignalR;

namespace Lx.Membership.SignalR.Hubs
{
    public class AuthenticationHub : MediatedHubBase
    {
        private readonly IOAuthClientSettings _oauthClientSettings;
        private readonly IOAuthClientService _service;

        public AuthenticationHub(IMediator mediator, ILogger logger, IMappingService mappingService,
            IRequestDispatchingProxy requestDispatchingProxy, IOAuthClientSettings oauthClientSettings,
            IOAuthClientService service, IOAuthHelper oauthHelper = null)
            : base(mediator, logger, mappingService, requestDispatchingProxy, oauthHelper)
        {
            _oauthClientSettings = oauthClientSettings;
            _service = service;
        }

        public async Task GetTokensAsync(GetTokensRequest request)
        {
            if ((request.OAuthLogin == null) || !request.OAuthLogin.IsValid)
                request.OAuthLogin = MappingService.Map<OAuthLogin>(_oauthClientSettings);

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
            if ((request.OAuthClient == null) || !request.OAuthClient.IsValid)
                request.OAuthClient = MappingService.Map<OAuthLoginClient>(_oauthClientSettings);

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
            if ((request.OAuthClient == null) || !request.OAuthClient.IsValid)
                request.OAuthClient = MappingService.Map<OAuthLoginClient>(_oauthClientSettings);

            await EnsureInGroupAsync(request);
            var response = await _service.RevokeTokenAsync(request);
            SendGroupResponse(response);
        }
    }
}