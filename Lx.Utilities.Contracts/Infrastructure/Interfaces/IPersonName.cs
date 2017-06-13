namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IPersonName
    {
        string FamilyName { get; }
        string GivenName { get; }
        string MiddleName { get; }
    }
}