using Lx.Utilities.Contracts.Authentication.Config;
using Lx.Utilities.Services.Config;

namespace Pec.Web.Config
{
    public class OAuthUris : IOAuthUris
    {
        public string BaseUriForAuthentication => this.AppSettingStringValue(x => x.BaseUriForAuthentication);
    }
}