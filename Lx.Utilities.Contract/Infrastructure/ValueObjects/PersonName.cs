using System.ComponentModel.DataAnnotations;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.ValueObjects {
    public class PersonName : IValueObject, IPersonName {
        public PersonName() { }

        public PersonName(string familyName, string givenName, string middleName = null) {
            FamilyName = familyName;
            GivenName = givenName;
            MiddleName = middleName;
        }

        [StringLength(100)]
        public string FamilyName { get; protected set; }

        [StringLength(100)]
        public string GivenName { get; protected set; }

        [StringLength(100)]
        public string MiddleName { get; protected set; }
    }
}