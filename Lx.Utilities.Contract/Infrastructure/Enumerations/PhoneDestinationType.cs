using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Infrastructure.Enumerations
{
    public class PhoneDestinationType : Enumeration
    {
        public PhoneDestinationType(int value, string name) : base(value, name)
        {
        }

        protected PhoneDestinationType()
        {
        }

        public static PhoneDestinationType Unknown => new PhoneDestinationType(0, nameof(Unknown));
        public static PhoneDestinationType Domestic => new PhoneDestinationType(10, nameof(Domestic));
        public static PhoneDestinationType International => new PhoneDestinationType(20, nameof(International));
    }
}