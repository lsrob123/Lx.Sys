namespace Lx.Utilities.Contract.Authentication.Interfaces {
    public interface IRoleProcess {
        string Name { get; }
        string Target { get; }
        bool IsDenied { get; }
    }
}