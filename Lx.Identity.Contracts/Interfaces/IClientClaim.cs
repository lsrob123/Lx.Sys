namespace Lx.Identity.Contracts.Interfaces {
    public interface IClientClaim {
        string Type { get; }
        string Value { get; }
    }
}