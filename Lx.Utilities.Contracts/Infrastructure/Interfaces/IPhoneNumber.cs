namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IPhoneNumber : IVerified
    {
        string LocalNumberWithAreaCode { get; }
        int? CountryCode { get; }
        string CountryName { get; }
        string FullNumber { get; }
    }
}