namespace Lx.Utilities.Contract.Authentication.Interfaces
{
    public interface IOAuthLoginClient
    {
        string ClientId { get; }
        string ClientSecret { get; }
        bool IsValid { get; }
    }
}