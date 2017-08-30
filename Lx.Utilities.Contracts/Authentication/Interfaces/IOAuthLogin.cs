namespace Lx.Utilities.Contracts.Authentication.Interfaces
{
    public interface IOAuthLogin : IOAuthLoginClient
    {
        string GrantType { get; }
        string Scopes { get; }
    }
}