namespace Lx.Identity.Contracts.Interfaces {
    public interface IClientScope : IHasClientKey
    {
        string Scope { get; }
    }
}