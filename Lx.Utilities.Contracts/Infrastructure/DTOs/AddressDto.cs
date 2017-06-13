using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public class AddressDto : IDto, IAddress
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CityOrSuburb { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string PoBox { get; set; }
        public bool Verified { get; set; }
    }
}