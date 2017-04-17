using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.SignalR
{
    public class SignalRConfig : ISignalRConfig
    {
        public string VirtualFolder => this.AppSettingStringValue(x => x.VirtualFolder);
    }
}