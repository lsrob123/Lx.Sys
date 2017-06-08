using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Shared.All.Domains.Identity.ValueObjects
{
    public class Address : IValueObject, IAddress
    {
        public bool Verified { get; protected set; }
        public string AddressLine1 { get; protected set; }
        public string AddressLine2 { get; protected set; }
        public string CityOrSuburb { get; protected set; }
        public string StateOrProvince { get; protected set; }
        public string Country { get; protected set; }
        public string PostCode { get; protected set; }
        public string PoBox { get; protected set; }
    }
}