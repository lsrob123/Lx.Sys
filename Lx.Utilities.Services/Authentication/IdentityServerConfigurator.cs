using System.Security.Cryptography.X509Certificates;
using Autofac;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Lx.Utilities.Contracts.Authentication.Config;

namespace Lx.Utilities.Services.Authentication
{
    public static class IdentityServerConfigurator
    {
        public static IdentityServerServiceFactory IdentityServerServiceFactory(IContainer container)
        {
            var config = container.Resolve<IIdentityServiceConfig>();

            var factory = new IdentityServerServiceFactory
            {
                UserService = new Registration<IUserService>(x => container.Resolve<IUserService>()),
                ClientStore = new Registration<IClientStore>(x => container.Resolve<IClientStore>()),
                ScopeStore = new Registration<IScopeStore>(x => container.Resolve<IScopeStore>()),
                CorsPolicyService = new Registration<ICorsPolicyService>(
                    c => new DefaultCorsPolicyService {AllowedOrigins = config.AllowedOrigins}),
                TokenHandleStore = new Registration<ITokenHandleStore>(x => container.Resolve<ITokenHandleStore>()),
                RefreshTokenStore = new Registration<IRefreshTokenStore>(x => container.Resolve<IRefreshTokenStore>()),
                ConsentStore = new Registration<IConsentStore>(x => container.Resolve<IConsentStore>()),
                //TODO: future test is required
                AuthorizationCodeStore =
                    new Registration<IAuthorizationCodeStore>(x => container.Resolve<IAuthorizationCodeStore>())
                //TODO: future test is required
            };

            return factory;
        }

        public static IdentityServerOptions IdentityServerOptions(IContainer container)
        {
            var config = container.Resolve<IIdentityServiceConfig>();

            var options = new IdentityServerOptions
            {
                Factory = IdentityServerServiceFactory(container),
                SigningCertificate = GetCertificate(container),
                SiteName = config.IdentityServiceSiteName,
                Endpoints = new EndpointOptions {EnableAccessTokenValidationEndpoint = false},
#if DEBUG
                LoggingOptions = new LoggingOptions
                {
                    EnableHttpLogging = true,
                    EnableWebApiDiagnostics = true,
                    WebApiDiagnosticsIsVerbose = true
                },
#endif
                RequireSsl = config.SslRequiredForIdentityServer
            };
            return options;
        }

        public static X509Certificate2 GetCertificate(IContainer container)
        {
            var config = container.Resolve<IIdentityServiceConfig>();
            var certificate = new X509Certificate2(config.CertificateFilePath, config.CertificateFilePassword);

            return certificate;
        }
    }
}