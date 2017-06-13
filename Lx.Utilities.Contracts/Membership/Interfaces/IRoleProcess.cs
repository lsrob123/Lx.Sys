namespace Lx.Utilities.Contracts.Membership.Interfaces
{
    public interface IRoleProcess
    {
        string Name { get; }
        string Target { get; }
        bool IsDenied { get; }
    }
}