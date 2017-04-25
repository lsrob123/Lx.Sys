using Lx.Utilities.Contract.Web;
using Lx.Utilities.Services.Config;

namespace Lx.Membership.Endpoint.Config
{
    public class WebEndpointSettings : IWebEndpointSettings
    {
        public string EndpointBaseUri => this.AppSettingStringValue(x => x.EndpointBaseUri);
    }
}