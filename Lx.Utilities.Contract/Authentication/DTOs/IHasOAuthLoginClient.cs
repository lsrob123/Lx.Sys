namespace Lx.Utilities.Contract.Authentication.DTOs {
    public interface IHasOAuthLoginClient {
        OAuthLoginClient OAuthClient { get; set; }
    }
}