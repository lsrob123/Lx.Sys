namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IPersonName {
        string FamilyName { get; }
        string GivenName { get; }
        string MiddleName { get; }
    }
}