using Lx.Utilities.Services.Config;

namespace Lx.Shared.All.Identity.Config {
    public class UserProfileConfig : IUserProfileConfig {
        public string UserProfileOriginator => this.AppSettingStringValue(x => x.UserProfileOriginator);
    }
}