using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contracts.Infrastructure.Domain;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.ValueObjects
{
    public class IpAddressSet : IValueObject, IIpAddressSet
    {
        public IpAddressSet()
        {
        }

        public IpAddressSet(string external, string @internal = null)
        {
            External = external;
            Internal = @internal;
        }

        [StringLength(50)]
        public string External { get; protected set; }

        [StringLength(50)]
        public string Internal { get; protected set; }
    }
}