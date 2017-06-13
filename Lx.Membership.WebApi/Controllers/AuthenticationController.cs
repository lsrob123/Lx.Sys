using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lx.Utilities.Contracts.Authentication.DTOs;
using Lx.Utilities.Contracts.Authentication.Interfaces;

namespace Lx.Membership.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class AuthenticationController : ApiController
    {
        private readonly IOAuthClientService _service;

        public AuthenticationController(IOAuthClientService service)
        {
            _service = service;
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
    }
}