using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lx.Utilities.Services.Persistence.DataAnnotation {
    public sealed class PhoneAllowsEmptyStringAttribute : DataTypeAttribute {
        private static readonly Regex _regex = CreateRegEx();

        public PhoneAllowsEmptyStringAttribute()
            : base(DataType.PhoneNumber) {
            ErrorMessage = ErrorMessageResourceName + "field is not a valid phone number";
        }

        public override bool IsValid(object value) {
            if ((value == null) || ((string) value == string.Empty))
                return true;
            var input = (string) value;
            if (_regex != null)
                return _regex.Match(input).Length > 0;
            var str = RemoveExtension(input.Replace("+", string.Empty).TrimEnd());
            var flag = str.Any(char.IsDigit);
            return flag && str.All(c => char.IsDigit(c) || char.IsWhiteSpace(c) || ("-.()".IndexOf(c) != -1));
        }

        private static Regex CreateRegEx() {
            var matchTimeout = TimeSpan.FromSeconds(2.0);
            try {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                    return
                        new Regex(
                            "^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$",
                            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled, matchTimeout);
            } catch {
                // ignored
            }
            return
                new Regex(
                    "^(\\+\\s?)?((?<!\\+.*)\\(\\+?\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+)([\\s\\-\\.]?(\\(\\d+([\\s\\-\\.]?\\d+)?\\)|\\d+))*(\\s?(x|ext\\.?)\\s?\\d+)?$",
                    RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        }

        private static string RemoveExtension(string potentialPhoneNumber) {
            var length1 = potentialPhoneNumber.LastIndexOf("ext.", StringComparison.InvariantCultureIgnoreCase);
            if ((length1 >= 0) && MatchesExtension(potentialPhoneNumber.Substring(length1 + 4)))
                return potentialPhoneNumber.Substring(0, length1);
            var length2 = potentialPhoneNumber.LastIndexOf("ext", StringComparison.InvariantCultureIgnoreCase);
            if ((length2 >= 0) && MatchesExtension(potentialPhoneNumber.Substring(length2 + 3)))
                return potentialPhoneNumber.Substring(0, length2);
            var length3 = potentialPhoneNumber.LastIndexOf("x", StringComparison.InvariantCultureIgnoreCase);
            if ((length3 >= 0) && MatchesExtension(potentialPhoneNumber.Substring(length3 + 1)))
                return potentialPhoneNumber.Substring(0, length3);
            return potentialPhoneNumber;
        }

        private static bool MatchesExtension(string potentialExtension) {
            potentialExtension = potentialExtension.TrimStart();
            return (potentialExtension.Length != 0) && potentialExtension.All(char.IsDigit);
        }
    }
}