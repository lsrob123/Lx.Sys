using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.ValueObject {
    public class IpAddressSet : IValueObject, IIpAddressSet {
        public IpAddressSet() {}

        public IpAddressSet(string external, string @internal = null) {
            External = external;
            Internal = @internal;
        }

        [StringLength(50)]
        public string External { get; protected set; }

        [StringLength(50)]
        public string Internal { get; protected set; }
    }
}