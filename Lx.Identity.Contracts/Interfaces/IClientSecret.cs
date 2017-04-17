namespace Lx.Identity.Contracts.Interfaces {
    public interface IClientSecret : IHasClientKey
    {
        string Value { get; }
    }
}