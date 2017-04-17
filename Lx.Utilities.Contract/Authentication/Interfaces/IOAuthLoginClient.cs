namespace Lx.Utilities.Contract.Authentication
{
    public interface IOAuthLoginClient
    {
        string ClientId { get; }
        string ClientSecret { get; }
        bool IsValid { get; }
    }
}