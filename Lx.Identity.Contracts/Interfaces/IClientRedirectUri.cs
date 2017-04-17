namespace Lx.Identity.Contracts.Interfaces
{
    public interface IClientRedirectUri : IHasClientKey
    {
        string Uri { get; }
    }
}