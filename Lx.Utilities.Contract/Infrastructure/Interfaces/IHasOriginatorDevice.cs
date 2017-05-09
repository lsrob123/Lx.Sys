using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Infrastructure.Interfaces {
    public interface IHasOriginatorDevice {
        DeviceDto OriginatorDevice { get; set; }
    }
}