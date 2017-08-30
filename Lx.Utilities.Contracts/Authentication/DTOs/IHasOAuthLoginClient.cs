namespace Lx.Utilities.Contracts.Authentication.DTOs
{
    public interface IHasOAuthLoginClient
    {
        OAuthLoginClient OAuthClient { get; set; }
    }
}