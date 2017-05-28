using System;
using System.Net;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public class GetTokensResponse : ResponseBase
    {
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorType { get; set; }
        public Exception Exception { get; set; }
        public long ExpiresIn { get; set; }
        public string HttpErrorReason { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string IdentityToken { get; set; }
        public bool IsError { get; set; }
        public string Raw { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public string RedirectUri { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}