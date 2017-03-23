using System.Collections.Generic;
using System.Linq;
using Lx.Utilities.Contract.Infrastructure.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.Helpers {
    public static class PhoneNumberHelper {
        public static string GetFullNumber(IPhoneNumber phoneNumberObject) {
            if (!phoneNumberObject.CountryCode.HasValue ||
                string.IsNullOrWhiteSpace(phoneNumberObject.LocalNumberWithAreaCode))
                return phoneNumberObject.LocalNumberWithAreaCode;
            var localNumber = GetLocalNumberInDigits(phoneNumberObject);
            return phoneNumberObject.CountryCode.Value + localNumber;
        }

        public static string GetLocalNumberInDigits(IPhoneNumber phoneNumberObject) {
            return GetNumberInDigits(phoneNumberObject.LocalNumberWithAreaCode);
        }

        public static string GetNumberInDigits(string originalNumber) {
            if (string.IsNullOrWhiteSpace(originalNumber))
                return originalNumber;

            var localNumberDigits = new List<char>(originalNumber.Where(c => {
                int digit;
                return int.TryParse(c.ToString(), out digit);
            })).ToArray();

            if (localNumberDigits.Length == 0)
                return null;

            var localNumber = long.Parse(new string(localNumberDigits)).ToString();
            return localNumber;
        }

        public static PhoneDestinationType GetPhoneDestinationType(IPhoneNumber phoneNumberObject, int? defCountryCode) {
            if (!phoneNumberObject.CountryCode.HasValue || (phoneNumberObject.CountryCode.Value == defCountryCode))
                return PhoneDestinationType.Domestic;
            return PhoneDestinationType.International;
        }
    }
}