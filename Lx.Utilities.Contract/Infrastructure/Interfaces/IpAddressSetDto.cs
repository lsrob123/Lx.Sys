namespace Lx.Utilities.Contract.Infrastructure.Interfaces
{
    public class IpAddressSetDto : IDto, IIpAddressSet
    {
        public string External { get; set; }
        public string Internal { get; set; }
    }
}