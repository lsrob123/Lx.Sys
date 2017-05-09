namespace Lx.Utilities.Contract.Authentication.Interfaces {
    public interface IOAuthLogin : IOAuthLoginClient {
        string GrantType { get; }
        string Scopes { get; }
    }
}