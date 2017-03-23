namespace Lx.Identity.Contracts.Interfaces {
    public interface IScopeClaim {
        string Name { get; }
        string Description { get; }
        bool AlwaysIncludeInIdToken { get; }
    }
}