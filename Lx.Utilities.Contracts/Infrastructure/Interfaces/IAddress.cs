namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IAddress : IVerified
    {
        string AddressLine1 { get; }
        string AddressLine2 { get; }
        string CityOrSuburb { get; }
        string StateOrProvince { get; }
        string Country { get; }
        string PostCode { get; }
        string PoBox { get; }
    }
}