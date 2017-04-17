using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.DTOs
{
    public class DeviceDto : IDto, IDevice
    {
        public string ManufacturerId { get; set; }
        public string OperatingSystem { get; set; }
        public string OperatingSystemVersion { get; set; }
        public string Model { get; set; }
    }
}