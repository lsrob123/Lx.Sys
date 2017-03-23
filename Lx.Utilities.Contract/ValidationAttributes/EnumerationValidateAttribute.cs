using System;
using System.ComponentModel.DataAnnotations;

namespace Lx.Utilities.Contract.ValidationAttributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class EnumerationValidateAttribute : ValidationAttribute {
        private readonly Type _acceptedEnumeration;

        public EnumerationValidateAttribute(Type acceptedEnumeration) {
            _acceptedEnumeration = acceptedEnumeration;
        }

        public override bool IsValid(object value) {
            var result = false;
            var inputStr = (string) value;
            if (_acceptedEnumeration.BaseType == null)
                return false;
            var method = _acceptedEnumeration.BaseType.GetMethod("FromName");

            var fromName = method.MakeGenericMethod(_acceptedEnumeration);
            var enumer = fromName.Invoke(_acceptedEnumeration,
                new object[] {inputStr, StringComparison.OrdinalIgnoreCase});
            if (enumer != null)
                result = true;
            return result;
        }
    }
}