using Lx.Utilities.Services.Config;

namespace Lx.Shared.All.Domains.Identity.Config {
    public class UserProfileConfig : IUserProfileConfig {
        public string UserProfileOriginator => this.AppSettingStringValue(x => x.UserProfileOriginator);
    }
}