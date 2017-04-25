using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IdentityModel.Client;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Interfaces;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Utilities.Services.Authentication
{
    public class OAuthClientService : IOAuthClientService
    {
        private readonly IOAuthUris _config;
        private readonly IMappingService _mappingService;

        public OAuthClientService(IOAuthUris config, IMappingService mappingService)
        {
            _config = config;
            _mappingService = mappingService;
        }

        public async Task<GetTokensResponse> GetTokensAsync(GetTokensRequest request)
        {
            if ((request.OAuthLogin == null) || !request.OAuthLogin.IsValid)
                return new GetTokensResponse()
                    .LinkTo(request)
                    .WithProcessResult(ProcessResultType.BadRequest,
                        $"Null reference of or invalid {nameof(request.OAuthLogin)}.");

            var tokenClient = new TokenClient(GetOAuthEndpointUri("token").AbsoluteUri, request.OAuthLogin.ClientId,
                request.OAuthLogin.ClientSecret);

            var result = await
                tokenClient.RequestResourceOwnerPasswordAsync(request.Username, request.Password,
                    request.OAuthLogin.Scopes);
            var response = CreateTokenResponse(request, result);

            return response;
        }

        public async Task<GetTokensResponse> RefreshTokensAsync(RefreshTokensRequest request)
        {
            if (!request.OAuthClient.IsValid)
                return new GetTokensResponse()
                    .LinkTo(request)
                    .WithProcessResult(ProcessResultType.BadRequest,
                        $"Null reference of or invalid {nameof(request.OAuthClient)}.");

            var tokenClient = new TokenClient(GetOAuthEndpointUri("token").AbsoluteUri, request.OAuthClient.ClientId,
                request.OAuthClient.ClientSecret);

            var result = await tokenClient.RequestRefreshTokenAsync(request.RefreshToken);
            var response = CreateTokenResponse(request, result);

            return response;
        }

        public async Task<GetUserInfoResponse> GetUserInfoAsync(GetUserInfoRequest request)
        {
            var userInfoClient = new UserInfoClient(GetOAuthEndpointUri("userinfo").AbsoluteUri);

            var result = await userInfoClient.GetAsync(request.AccessToken);
            var response = _mappingService.Map<GetUserInfoResponse>(result).LinkTo(request);

            return response;
        }

        public async Task<RevokeTokenResponse> RevokeTokenAsync(RevokeTokenRequest request)
        {
            if (!request.OAuthClient.IsValid)
                return new RevokeTokenResponse().LinkTo(request)
                    .WithProcessResult(ProcessResultType.BadRequest,
                        $"Null reference of or invalid {nameof(request.OAuthClient)}.");

            var response = new RevokeTokenResponse().LinkTo(request);

            var uri = GetOAuthEndpointUri("revocation");
            var webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var auth = "Basic " + Base64Encode($"{request.OAuthClient.ClientId}:{request.OAuthClient.ClientSecret}");
            webClient.Headers.Add("Authorization", auth);

            var formData = new NameValueCollection
            {
                {"token", request.AccessTokenOrRefreshToken},
                {"token_type_hint", request.TokenHint}
            };

            try
            {
                await webClient.UploadValuesTaskAsync(uri, "POST", formData);
                response.WithProcessResult(ProcessResultType.Ok);
            }
            catch (HttpException httpException)
            {
                response.WithProcessResult(Enumeration.FromValue<ProcessResultType>(httpException.GetHttpCode()),
                    httpException.GetHtmlErrorMessage());
            }
            catch (Exception execption)
            {
                response.WithProcessResult(execption);
            }

            return response;
        }

        private GetTokensResponse CreateTokenResponse(IBasicRequestKey request, TokenResponse result)
        {
            var response = _mappingService.Map<GetTokensResponse>(result).LinkTo(request);

            var exceptions = new List<Exception>();
            if (!string.IsNullOrWhiteSpace(result.Error))
                exceptions.Add(new Exception($"{result.ErrorType} - {result.Error} - {result.ErrorDescription}"));
            if (result.Exception != null)
                exceptions.Add(result.Exception);
            if (!string.IsNullOrWhiteSpace(result.HttpErrorReason))
                exceptions.Add(new HttpException((int) result.HttpStatusCode, result.HttpErrorReason));

            if (exceptions.Any())
                response.WithProcessResult(exceptions);
            else
                response.WithProcessResult(ProcessResultType.Ok);

            return response;
        }

        private Uri GetOAuthEndpointUri(string endpointName)
        {
            var uri = new Uri(new Uri(_config.BaseUriForAuthentication, UriKind.Absolute),
                new Uri(endpointName, UriKind.Relative));
            return uri;
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}