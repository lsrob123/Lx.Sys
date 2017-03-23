using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public class DeviceDto : IDto, IDevice {
        public string ManufacturerId { get; set; }
        public string OperatingSystem { get; set; }
        public string OperatingSystemVersion { get; set; }
        public string Model { get; set; }
    }
}