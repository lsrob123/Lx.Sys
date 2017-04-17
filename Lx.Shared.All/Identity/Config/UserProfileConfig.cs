using Lx.Utilities.Services.Config;

namespace Lx.Shared.All.Identity.Config
{
    public class UserProfileConfig : IUserProfileConfig
    {
        public string Originator => this.AppSettingStringValue(x => x.Originator);
    }
}