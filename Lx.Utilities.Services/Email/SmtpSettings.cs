using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.Email
{
    public class SmtpSettings : ISmtpSettings
    {
        public bool IsRealSend => this.AppSettingBooleanValue(x => x.IsRealSend);
        public bool IsSsl => this.AppSettingBooleanValue(x => x.IsSsl);
        public string Username => this.AppSettingStringValue(x => x.Username);
        public string Password => this.AppSettingStringValue(x => x.Password);
        public string Host => this.AppSettingStringValue(x => x.Host);
        public int Port => this.AppSettingIntValue(x => x.Port);
    }
}