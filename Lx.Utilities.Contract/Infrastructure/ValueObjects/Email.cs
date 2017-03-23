using System;
using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.ValueObjects {
    public class Email : IValueObject, IEmail, IEquatable<Email> {
        [StringLength(200)]
        public string Address { get; protected set; }

        public bool Verified { get; protected set; }

        public bool Equals(Email other) {
            if (other == null)
                return false;

            var areEqual = Address.Equals(other.Address, StringComparison.OrdinalIgnoreCase)
                           && (Verified == other.Verified);
            return areEqual;
        }
    }
}