using System;

namespace Lx.Identity.Contracts.DTOs
{
    public class CreateVerificationCodeResponse : ResponseBase
    {
        public VerificationPurpose VerificationPurpose { get; set; }
        public string VerificationCode { get; set; }
        public string PlainTextPassword { get; set; }
        public DateTimeOffset? TimeVerificationCodeExpires { get; set; }

        public double ExpiresInMinutes
        {
            get
            {
                if (!TimeVerificationCodeExpires.HasValue)
                    return 0;

                var timeToExpiration = TimeVerificationCodeExpires.Value.Subtract(DateTimeOffset.UtcNow);
                var timeToExpirationMinutes = timeToExpiration <= TimeSpan.Zero ? 0 : timeToExpiration.TotalMinutes;
                return timeToExpirationMinutes;
            }
        }

        public override void EraseShareGroupInfoForClientSide()
        {
        }

        public override void EnsureSecurityForClientSide()
        {
            VerificationCode = null;
            PlainTextPassword = null;
            base.EnsureSecurityForClientSide();
        }

        public CreateVerificationCodeResponse LinkTo(IBasicRequestKey request,
            VerificationPurpose verificationPurpose)
        {
            this.LinkTo(request);
            VerificationPurpose = verificationPurpose;
            return this;
        }

        public CreateVerificationCodeResponse WithVerificationCode(VerificationPurpose verificationPurpose,
            string verificationCode, DateTimeOffset timeVerificationCodeExpires,
            DateTimeOffset? timeVerificationCodeSent = null)
        {
            VerificationPurpose = verificationPurpose;
            VerificationCode = verificationCode;
            TimeVerificationCodeExpires = timeVerificationCodeExpires;
            return this;
        }
    }
}