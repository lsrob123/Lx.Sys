using System;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.Events
{
    public class PasswordResetVerificationCodeEvent : EventBase
    {
        public string PlainTextVerificationCode { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}