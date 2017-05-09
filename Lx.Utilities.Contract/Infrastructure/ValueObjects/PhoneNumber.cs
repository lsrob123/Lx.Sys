using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.Helpers;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.ValueObjects {
    public class PhoneNumber : IValueObject, IPhoneNumber, IEquatable<PhoneNumber> {
        public PhoneNumber() { }

        public PhoneNumber(string localNumberWithAreaCode, bool verified, int? countryCode = null,
            string countryName = null) {
            LocalNumberWithAreaCode = localNumberWithAreaCode;
            SetVerified(verified);
            CountryCode = countryCode;
            CountryName = countryName;
            LocalNumberWithAreaCodeInDigits = LocalNumberWithAreaCode.GetNumberInDigits();
        }

        [StringLength(100)]
        public string LocalNumberWithAreaCodeInDigits { get; protected set; }

        public bool Equals(PhoneNumber other) {
            if (other == null)
                return false;

            return FullNumber.Equals(other.FullNumber) && Verified == other.Verified;
        }

        [StringLength(100)]
        public string LocalNumberWithAreaCode { get; protected set; }

        public int? CountryCode { get; protected set; }

        [StringLength(100)]
        public string CountryName { get; protected set; }

        public bool Verified { get; protected set; }

        [NotMapped]
        public string FullNumber => this.GetFullNumber();

        public void SetVerified(bool verified) {
            Verified = verified;
        }
    }
}