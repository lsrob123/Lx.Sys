using System;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection;

namespace Lx.Utilities.Services.Config
{
    public static class ConfigExtensions
    {
        public static string AppSettingKey<TConfig>(TConfig configObject,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var configType = configObject.GetType();

            var memberExpression = propertyGetter.Body as MemberExpression;
            if (memberExpression == null)
            {
                if (!propertyGetter.Body.NodeType.Equals(ExpressionType.Convert))
                    return null;

                var unaryExpression = propertyGetter.Body as UnaryExpression;
                if (unaryExpression == null)
                    return null;

                memberExpression = unaryExpression.Operand as MemberExpression;
                if (memberExpression == null)
                    return null;
            }

            var member = memberExpression.Member;
            if (!(member is FieldInfo || member is PropertyInfo))
                return null;

            return configType.Name + separator + member.Name;
        }

        /// <summary>
        ///     Extension for extracting an appSetting from the config file's appSettings section
        /// </summary>
        /// <typeparam name="TConfig">Type of the config class</typeparam>
        /// <param name="config">Self reference to the </param>
        /// <param name="propertyGetter">Expression for a factory returning the nominated property</param>
        /// <param name="separator">Sparator between the config type name and the property name in return value</param>
        /// <returns>Value of the appSetting</returns>
        public static string AppSettingStringValue<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var key = AppSettingKey(config, propertyGetter, separator);
            var value = ConfigurationManager.AppSettings[key];
            return value;
        }

        public static int AppSettingIntValue<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var value = AppSettingStringValue(config, propertyGetter, separator);
            int targetValue;
            return int.TryParse(value, out targetValue) ? targetValue : 0;
        }

        public static decimal AppSettingDecimalValue<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var value = AppSettingStringValue(config, propertyGetter, separator);
            decimal targetValue;
            return decimal.TryParse(value, out targetValue) ? targetValue : 0M;
        }

        public static Guid AppSettingGuidValue<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var value = AppSettingStringValue(config, propertyGetter, separator);
            Guid targetValue;
            return Guid.TryParse(value, out targetValue) ? targetValue : Guid.Empty;
        }

        public static TValue? AppSettingNullableValue<TConfig, TValue>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter, string separator = ".")
            where TValue : struct
        {
            var stringValue = AppSettingStringValue(config, propertyGetter, separator);
            if (string.IsNullOrWhiteSpace(stringValue))
                return null;

            var value = (TValue) Convert.ChangeType(stringValue, typeof (TValue));

            return value;
        }

        /// <typeparam name="TConfig">Type of the config class</typeparam>
        /// <param name="config">Self reference to the </param>
        /// <param name="propertyGetter">Expression for a factory returning the nominated property</param>
        /// <param name="separator">Sparator between the config type name and the property name in return value</param>
        /// <returns>Value of the appSetting</returns>
        public static bool AppSettingBooleanValue<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var value = AppSettingStringValue(config, propertyGetter, separator);
            return GetBoolValue<TConfig>(value);
        }

        private static bool GetBoolValue<TConfig>(string value)
        {
            var boolValue = !string.IsNullOrWhiteSpace(value) &&
                            (value.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                             value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                             value.Equals("1", StringComparison.OrdinalIgnoreCase));
            return boolValue;
        }

        //private static int GetIntValue<TConfig>(string value)
        //{
        //    int res;
        //    return int.TryParse(value, out res) ? res : 0;
        //}

        /// <typeparam name="TConfig">Type of the config class</typeparam>
        /// <param name="config">Self reference to the </param>
        /// <param name="propertyGetter">Expression for a factory returning the nominated property</param>
        /// <param name="separator">Sparator between the config type name and the property name in return value</param>
        /// <returns>Value of the appSetting</returns>
        public static bool? AppSettingNullableBooleanValue<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            string separator = ".")
        {
            var value = AppSettingStringValue(config, propertyGetter, separator);
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return GetBoolValue<TConfig>(value);
        }

        public static TimeSpan AppSettingTimeSpanFromDaysOrMinutes<TConfig>(this TConfig config,
            Expression<Func<TConfig, object>> propertyGetter,
            int defaultMinutes = 0, int defaultDays = 0, string separator = ".")
        {
            var defaultValue = defaultMinutes == 0
                ? (defaultDays == 0 ? TimeSpan.Zero : TimeSpan.FromDays(defaultDays))
                : TimeSpan.FromMinutes(defaultMinutes);

            var text = AppSettingStringValue(config, propertyGetter, separator);

            if (string.IsNullOrWhiteSpace(text))
                return defaultValue;

            int lengthInDigits;
            Func<int, TimeSpan> getTimeSpan = x => TimeSpan.FromDays(x);
            var lengthInText = text.ToLower();

            if (lengthInText.Contains("m") || lengthInText.Contains("min") || lengthInText.Contains("minutes"))
            {
                lengthInText = lengthInText.Replace("minutes", string.Empty)
                    .Replace("min", string.Empty).Replace("m", string.Empty);
                getTimeSpan = x => TimeSpan.FromMinutes(x);
            }
            else if (lengthInText.Contains("d") || lengthInText.Contains("days"))
            {
                lengthInText = lengthInText.Replace("days", string.Empty).Replace("d", string.Empty);
            }

            return int.TryParse(lengthInText.Trim(), out lengthInDigits)
                ? getTimeSpan(lengthInDigits)
                : defaultValue;
        }
    }
}