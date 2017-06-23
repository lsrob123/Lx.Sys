using System;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.RequestsResponses
{
    public class ResetPasswordRequest : RequestBase
    {
        public Guid UserKey { get; set; }
        public string PlainTextVerificationCode { get; set; }
        public string NewPlainTextPassword { get; set; }
    }
}