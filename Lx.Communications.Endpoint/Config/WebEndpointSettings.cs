using Lx.Utilities.Contracts.Web;
using Lx.Utilities.Services.Config;

namespace Lx.Communications.Endpoint.Config
{
    public class WebEndpointSettings : IWebEndpointSettings
    {
        public string EndpointBaseUri => this.AppSettingStringValue(x => x.EndpointBaseUri);
    }
}