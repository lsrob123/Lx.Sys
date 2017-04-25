using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Services.Config;

namespace Lx.Membership.Endpoint.Config
{
    public class OAuthUris : IOAuthUris
    {
        public string BaseUriForAuthentication => this.AppSettingStringValue(x => x.BaseUriForAuthentication);
    }
}