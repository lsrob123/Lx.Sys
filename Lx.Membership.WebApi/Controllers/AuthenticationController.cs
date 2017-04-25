using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Lx.Utilities.Contract.Authentication.DTOs;
using Lx.Utilities.Contract.Authentication.Interfaces;

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
        [HttpGet]
        public async Task<HttpResponseMessage> GetTokensAsync(GetTokensRequest request)
        {
            var response = await _service.GetTokensAsync(request);
            return Request.CreateResponse(response);
        }

        [Route("refresh-tokens")]
        [HttpGet]
        public async Task<HttpResponseMessage> RefreshTokensAsync(RefreshTokensRequest request)
        {
            var response = await _service.RefreshTokensAsync(request);
            return Request.CreateResponse(response);
        }

        [Route("user-info")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUserInfoAsync(GetUserInfoRequest request)
        {
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