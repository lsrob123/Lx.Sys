using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lx.Membership.Services.APIs;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Extensions;

namespace Lx.Membership.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class AuthenticationController : ApiController
    {
        private readonly IOAuthClientService _service;
        private readonly IAuthenticationApi _authenticationApi;

        public AuthenticationController(IOAuthClientService service, IAuthenticationApi authenticationApi)
        {
            _service = service;
            _authenticationApi = authenticationApi;
        }

        [Route("time")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCurrentTime()
        {
            return await Task.Run(() => Request.CreateResponse(DateTimeOffset.UtcNow.ToString()));
        }


        [Route("tokens")]
        [HttpPost]
        public async Task<HttpResponseMessage> GetTokensAsync(GetTokensRequest request)
        {
            var response = await _service.GetTokensAsync(request);
            return Request.CreateResponse(response);
        }

        [Route("refresh-tokens")]
        [HttpPost]
        public async Task<HttpResponseMessage> RefreshTokensAsync(RefreshTokensRequest request)
        {
            var response = await _service.RefreshTokensAsync(request);
            return Request.CreateResponse(response);
        }

        [Route("tokens/{accessToken}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUserInfoAsync(string accessToken)
        {
            var request = new GetUserInfoRequest
            {
                AccessToken = accessToken
            };
            var response = await _service.GetUserInfoAsync(request);
            return Request.CreateResponse(response);
        }

        [Route("tokens")]
        [HttpDelete]
        public async Task<HttpResponseMessage> RevokeTokenAsync(RevokeTokenRequest request)
        {
            var response = await _service.RevokeTokenAsync(request);
            return Request.CreateResponse(response);
        }

        [Route("users/{userKey}/verifications/{plainTextVerificationCode}")]
        [HttpPost]
        public async Task<HttpResponseMessage> ResetPassword(Guid userKey, string plainTextVerificationCode)
        {
            var formData = await Request.Content.ReadAsFormDataAsync();
            if (formData == null || !formData.HasKeys() || string.IsNullOrWhiteSpace(formData.Get("password")))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No password was posted with a form.");
            }

            var request=new ResetPasswordRequest
            {
                UserKey = userKey,
                PlainTextVerificationCode = plainTextVerificationCode,
                NewPlainTextPassword = formData["password"]
            }.WithUser(userKey);

            _authenticationApi.Start(request);
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

    }
}