using Lx.Utilities.Services.Config;

namespace Lx.Shared.All.Domains.Identity.Config
{
    public class CommonBusEndpointSettings : ICommonBusEndpointSettings
    {
        public string Identity => this.AppSettingStringValue(x => x.Identity);
        public string Communications => this.AppSettingStringValue(x => x.Communications);
    }
}