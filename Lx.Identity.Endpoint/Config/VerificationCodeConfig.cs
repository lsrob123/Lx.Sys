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

        protected int PasswordResetVerificationCodeLiveSpanMinutes
            => this.AppSettingIntValue(x => PasswordResetVerificationCodeLiveSpanMinutes);

        protected int MobileVerificationCodeLiveSpanMinutes
            => this.AppSettingIntValue(x => MobileVerificationCodeLiveSpanMinutes);

        public TimeSpan EmailVerificationCodeLiveSpan => TimeSpan.FromMinutes(EmailVerificationCodeLiveSpanMinutes);
        public TimeSpan MobileVerificationCodeLiveSpan => TimeSpan.FromMinutes(MobileVerificationCodeLiveSpanMinutes);

        public TimeSpan PasswordResetVerificationCodeLiveSpan
            => TimeSpan.FromMinutes(PasswordResetVerificationCodeLiveSpanMinutes);

        public TimeSpan GetLiveSpan(VerificationPurpose purpose)
        {
            if (purpose.Equals(VerificationPurpose.VerifyEmail))
                return EmailVerificationCodeLiveSpan;
            if (purpose.Equals(VerificationPurpose.VerifyMobile))
                return MobileVerificationCodeLiveSpan;
            return purpose.Equals(VerificationPurpose.PasswordlessLogin)
                ? PasswordResetVerificationCodeLiveSpan
                : TimeSpan.Zero;
        }
    }
}