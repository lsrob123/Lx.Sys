namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public interface IDevice
    {
        string ManufacturerId { get; }
        string OperatingSystem { get; }
        string OperatingSystemVersion { get; }
        string Model { get; }
    }
}