using System.Text;
using Lx.Utilities.Contract.Infrastructure.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Infrastructure.Extensions {
    public static class PersonNameExtensions {
        public static string FullName<TPersonName>(this TPersonName personName, Locale locale = null)
            where TPersonName : IPersonName {
            locale = locale ?? Locale.English;

            var fullNameBuilder = new StringBuilder();
            if (locale.Equals(Locale.Chinese)) {
                if (!string.IsNullOrWhiteSpace(personName.FamilyName))
                    fullNameBuilder.Append(personName.FamilyName.Trim());
                if (!string.IsNullOrWhiteSpace(personName.MiddleName))
                    fullNameBuilder.Append(personName.MiddleName.Trim());
                if (!string.IsNullOrWhiteSpace(personName.GivenName))
                    fullNameBuilder.Append(personName.GivenName.Trim());
            } else {
                if (!string.IsNullOrWhiteSpace(personName.GivenName))
                    fullNameBuilder.Append(personName.GivenName.Trim() + " ");
                if (!string.IsNullOrWhiteSpace(personName.MiddleName))
                    fullNameBuilder.Append(personName.MiddleName.Trim() + " ");
                if (!string.IsNullOrWhiteSpace(personName.FamilyName))
                    fullNameBuilder.Append(personName.FamilyName.Trim() + " ");
            }
            return fullNameBuilder.ToString().Trim();
        }
    }
}