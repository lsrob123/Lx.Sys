using System;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Shared.All.Domains.Identity.Events
{
    public class VerificationCodeCreatedEvent : EventBase
    {
        public Guid UserKey { get; set; }
        public EmailParticipant Recipient { get; set; }
        public VerificationPurpose VerificationPurpose { get; set; }
        public string PlainTextVerificationCode { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }

        public override void EraseShareGroupInfoForClientSide()
        {
        }
    }
}