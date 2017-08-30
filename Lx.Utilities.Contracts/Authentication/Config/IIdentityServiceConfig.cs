namespace Lx.Utilities.Contracts.Authentication.Config
{
    public interface IIdentityServiceConfig
    {
        string IdentityProviderName { get; }
        string IdentityServiceSiteName { get; }
        string CertificateFilePath { get; }
        string CertificateFilePassword { get; }
        bool SslRequiredForIdentityServer { get; }
        string[] AllowedOrigins { get; }
    }
}