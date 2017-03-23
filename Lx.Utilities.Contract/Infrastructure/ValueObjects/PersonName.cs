using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;

namespace Lx.Utilities.Contract.Infrastructure.ValueObjects {
    public class PersonName : IValueObject {
        [StringLength(100)]
        public string FamilyName { get; protected set; }

        [StringLength(100)]
        public string GivenName { get; protected set; }

        [StringLength(100)]
        public string MiddleName { get; protected set; }
    }
}