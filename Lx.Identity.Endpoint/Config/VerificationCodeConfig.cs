using System;
using Lx.Identity.Contracts.Config;
using Lx.Utilities.Contracts.Authentication.Enumerations;
using Lx.Utilities.Services.Config;

namespace Lx.Identity.Endpoint.Config
{
    public class VerificationCodeConfig : IVerificationCodeConfig
    {
        protected int EmailVerificationCodeLiveSpanMinutes
            => this.AppSettingIntValue(x => EmailVerificationCodeLiveSpanMinutes);

        protected int PasswordlessVerificationCodeLiveSpanMinutes
            => this.AppSettingIntValue(x => PasswordlessVerificationCodeLiveSpanMinutes);

        protected int MobileVerificationCodeLiveSpanMinutes
            => this.AppSettingIntValue(x => MobileVerificationCodeLiveSpanMinutes);

        public TimeSpan EmailVerificationCodeLiveSpan => TimeSpan.FromMinutes(EmailVerificationCodeLiveSpanMinutes);
        public TimeSpan MobileVerificationCodeLiveSpan => TimeSpan.FromMinutes(MobileVerificationCodeLiveSpanMinutes);

        public TimeSpan PasswordlessLoginVerificationCodeLiveSpan
            => TimeSpan.FromMinutes(PasswordlessVerificationCodeLiveSpanMinutes);

        public TimeSpan GetLiveSpan(VerificationPurpose purpose)
        {
            if (purpose.Equals(VerificationPurpose.VerifyEmail))
                return EmailVerificationCodeLiveSpan;
            if (purpose.Equals(VerificationPurpose.VerifyMobile))
                return MobileVerificationCodeLiveSpan;
            return purpose.Equals(VerificationPurpose.PasswordlessLogin)
                ? PasswordlessLoginVerificationCodeLiveSpan
                : TimeSpan.Zero;
        }
    }
}