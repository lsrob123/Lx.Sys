using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Authentication.DTOs
{
    public class GetUserInfoResponse : ResponseBase
    {
        public IEnumerable<Claim> Claims { get; set; }
        public string Error { get; set; }
        public string ErrorType { get; set; }
        public Exception Exception { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool IsError { get; set; }
        public string Raw { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}