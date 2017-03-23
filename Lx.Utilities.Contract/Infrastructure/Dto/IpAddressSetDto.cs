using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public class IpAddressSetDto : IDto, IIpAddressSet {
        public string External { get; set; }
        public string Internal { get; set; }
    }
}