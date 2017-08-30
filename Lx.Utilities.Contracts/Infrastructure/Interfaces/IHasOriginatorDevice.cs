using Lx.Utilities.Contracts.Infrastructure.DTOs;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IHasOriginatorDevice
    {
        DeviceDto OriginatorDevice { get; set; }
    }
}