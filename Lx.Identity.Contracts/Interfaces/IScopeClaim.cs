namespace Lx.Identity.Contracts.Interfaces
{
    public interface IScopeClaim : IHasScopeKey
    {
        string Name { get; }
        string Description { get; }
        bool AlwaysIncludeInIdToken { get; }
    }
}