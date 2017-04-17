using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lx.Utilities.Services.Reflection
{
    public static class ReflectionExtensions
    {
        public static void AssignPropertyValuesTo(this object source, object destination,
            params string[] excludedPropertyNames)
        {
            var sourceType = source.GetType();
            var destinationType = destination.GetType();
            var excluded = new List<string>(excludedPropertyNames);

            var sourceProperties = sourceType.GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                if (excluded.Any(x => sourceProperty.Name.Equals(x, StringComparison.OrdinalIgnoreCase)))
                    continue;

                var destinationProperty = destinationType.GetProperty(sourceProperty.Name);
                if (destinationProperty == null)
                    continue;

                var value = sourceProperty.GetValue(source);
                destinationProperty.SetValue(destination, value);
            }
        }

        public static ICollection<Type> GetMethodParameterTypes(Type objectType, string methodName)
        {
            var methodInfo = objectType.GetMethod(methodName);
            var parameterTypes = methodInfo.GetParameters().Select(x => x.ParameterType).ToList();
            return parameterTypes;
        }

        public static ICollection<Type> GetMethodParameterTypes(this object source, string methodName)
        {
            return GetMethodParameterTypes(source.GetType(), methodName);
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this TSource source,
            Expression<Func<TSource, TProperty>> propertyGetterExpression)
        {
            var type = typeof (TSource);

            var member = propertyGetterExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(
                    $"Expression '{propertyGetterExpression}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyGetterExpression}' refers to a field, not a property.");

            if ((propInfo.ReflectedType != null) && (type != propInfo.ReflectedType) &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(
                    $"Expresion '{propertyGetterExpression}' refers to a property that is not from type {type}.");

            return propInfo;
        }
    }
}