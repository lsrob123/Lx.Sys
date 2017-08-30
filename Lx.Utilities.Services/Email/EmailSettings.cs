using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.Email
{
    public class EmailSettings : IEmailSettings
    {
        public bool DumpToFilesOnly => this.AppSettingBooleanValue(x => x.DumpToFilesOnly);
        public string DumpFileFolder => this.AppSettingStringValue(x => x.DumpFileFolder);
    }
}