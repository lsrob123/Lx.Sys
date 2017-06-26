using Lx.Utilities.Contracts.Web;
using Lx.Utilities.Services.Config;

namespace Pec.Web.Config
{
    public class WebAuthenticationSettings : IWebAuthenticationSettings
    {
        public string AccessTokenCookie => nameof(AccessTokenCookie);
        public string RefreshTokenCookie => nameof(RefreshTokenCookie);
        public string AccessTokenExpiryToken => nameof(AccessTokenExpiryToken);
        public string LoginPageUrl => this.AppSettingStringValue(x => x.LoginPageUrl);
    }
}