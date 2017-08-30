using System;
using AutoMapper;
using Lx.Utilities.Contracts.Infrastructure.Common;

namespace Lx.Utilities.Services.Mapping.AutoMapper
{
    public class MapSetting
    {
        public MapSetting(Type source, Type destination,
            Func<IMappingExpression, IMappingExpression> customMapFunc = null)
        {
            Source = source;
            Destination = destination;
            CustomMap = customMapFunc;
        }

        //public MapSetting(Type source, Type destination, Action<IMappingExpression> customMapAction = null)
        //{
        //    Source = source;
        //    Destination = destination;
        //    CustomMap = expression =>
        //    {
        //        customMapAction?.Invoke(expression);
        //        return expression;
        //    };
        //}

        public Type Source { get; }
        public Type Destination { get; }
        public Func<IMappingExpression, IMappingExpression> CustomMap { get; }

        public static MapSetting<TEnumeration, TEnum> EnumerationToEnumMap<TEnumeration, TEnum>()
            where TEnumeration : Enumeration
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            return new MapSetting<TEnumeration, TEnum>(exp => exp.ConstructUsing(x =>
                (TEnum) Enum.Parse(typeof(TEnum), ((TEnumeration) x).Name)));
        }

        public static MapSetting<TEnumeration, string> EnumerationToStringMap<TEnumeration>()
            where TEnumeration : Enumeration
        {
            return new MapSetting<TEnumeration, string>(
                exp => exp.ConstructUsing(x => ((TEnumeration) x).Name));
        }

        public static MapSetting<string, TEnumeration> StringToEnumerationMap<TEnumeration>()
            where TEnumeration : Enumeration
        {
            return new MapSetting<string, TEnumeration>(
                exp => exp.ConstructUsing(x => Enumeration.FromName<TEnumeration>((string) x)));
        }
    }

    public class MapSetting<TSource, TDestination> : MapSetting
    {
        public MapSetting(Func<IMappingExpression, IMappingExpression> customMapFunc = null)
            : base(typeof(TSource), typeof(TDestination), customMapFunc)
        {
        }
    }
}