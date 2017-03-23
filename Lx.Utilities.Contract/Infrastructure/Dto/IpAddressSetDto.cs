using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public class IpAddressSetDto : IDto, IIpAddressSet {
        public string External { get; set; }
        public string Internal { get; set; }
    }
}