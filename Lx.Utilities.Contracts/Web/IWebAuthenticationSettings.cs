namespace Lx.Utilities.Contracts.Web
{
    public interface IWebAuthenticationSettings
    {
        string AccessTokenCookie { get; }
        string RefreshTokenCookie { get; }
        string LoginPageUrl { get; }
        string AccessTokenExpiryToken { get; }
    }
}