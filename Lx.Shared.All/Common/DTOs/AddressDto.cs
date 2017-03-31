namespace Lx.Shared.All.Common.DTOs {
    public class AddressDto : IDto, IAddress {
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