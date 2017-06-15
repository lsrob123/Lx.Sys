using System;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Contracts.Authentication.Interfaces;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public class VerificationFields : IHasVerificationFields, IDto
    {
        public VerificationPurpose VerificationPurpose { get; set; }
        public string HashedVerificationCode { get; set; }
        public DateTimeOffset? TimeVerificationCodeSent { get; set; }
        public DateTimeOffset? TimeVerificationCodeExpires { get; set; }
    }
}