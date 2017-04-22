namespace Lx.Utilities.Contract.Authentication.Interfaces
{
    public interface IRoleProcess
    {
        string Name { get; set; }
        string Target { get; set; }
        bool IsDenied { get; set; }
    }
}