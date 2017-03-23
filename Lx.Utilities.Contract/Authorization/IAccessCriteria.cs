namespace Lx.Utilities.Contract.Authorization {
    public interface IAccessCriteria {
        string Process { get; set; }
        string Target { get; set; }
        bool IsDenied { get; set; }
        string Roles { get; set; }
        string Users { get; set; }
    }
}