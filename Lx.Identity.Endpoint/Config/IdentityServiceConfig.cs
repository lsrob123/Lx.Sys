using System;
using Lx.Identity.Contracts.Config;
using Lx.Utilities.Contract.Authentication.Config;
using Lx.Utilities.Services.Config;

namespace Lx.Identity.Endpoint.Config {
    public class IdentityServiceConfig : IIdentityServiceConfig {
        public string IdentityProviderName => this.AppSettingStringValue(x => x.IdentityProviderName);
        public string IdentityServiceSiteName => this.AppSettingStringValue(x => x.IdentityServiceSiteName);
        public string CertificateFilePath => this.AppSettingStringValue(x => x.CertificateFilePath);
        public string CertificateFilePassword => this.AppSettingStringValue(x => x.CertificateFilePassword);

        public bool SslRequiredForIdentityServer => this.AppSettingBooleanValue(x => x.SslRequiredForIdentityServer);

        public string[] AllowedOrigins
            => this.AppSettingStringValue(x => x.AllowedOrigins)
                .Split(new[] {""}, StringSplitOptions.RemoveEmptyEntries);
    }
}