using System;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Contract.Authentication.Constants;
using Lx.Utilities.Services.Config;

namespace Lx.Utilities.Services.Authentication {
    public class OAuthClientSettings : IOAuthClientSettings {
        public string BaseUri => this.AppSettingStringValue(x => x.BaseUri);
        public string UserInfoEndpointAbsolutePath => this.AppSettingStringValue(x => x.UserInfoEndpointAbsolutePath);
        public string DefaultClientId => this.AppSettingStringValue(x => x.DefaultClientId);
        public string DefaultClientSecret => this.AppSettingStringValue(x => x.DefaultClientSecret);
        public string DefaultScopes => this.AppSettingStringValue(x => x.DefaultScopes);
        public string DefaultGrantType => GrantType.Password;

        public TimeSpan? AccessTokenValidationResultLifeSpan
            =>
            TimeSpan.FromSeconds(double.Parse(this.AppSettingStringValue(x => x.AccessTokenValidationResultLifeSpan)));
    }
}