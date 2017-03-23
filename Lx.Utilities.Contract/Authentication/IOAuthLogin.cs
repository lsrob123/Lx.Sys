namespace Lx.Utilities.Contract.Authentication {
    public interface IOAuthLogin : IOAuthLoginClient {
        string GrantType { get; }
        string Scopes { get; }
    }
}