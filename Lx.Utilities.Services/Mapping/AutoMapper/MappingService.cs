using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Utilities.Services.Mapping.AutoMapper
{
    public class MappingService : IMappingService
    {
        protected static readonly ConcurrentBag<MapSetting> MapSettings = new ConcurrentBag<MapSetting>();

        public TDestination Map<TDestination>(object source)
        {
            if (source == null)
                return default(TDestination);

            var mappedObject = Mapper.Map<TDestination>(source);
            (mappedObject as IEntity)?.AssignDefaultValuesToComplexPropertiesIfNull();

            return mappedObject;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
                return default(TDestination);

            var mappedObject = Mapper.Map<TSource, TDestination>(source);
            (mappedObject as IEntity)?.AssignDefaultValuesToComplexPropertiesIfNull();

            return mappedObject;
        }

        //public object Map(object source, Type destinationType)
        //{
        //    var mappedObject = Mapper.Map(source.GetType(), destinationType);
        //    var entity = mappedObject as IEntity;

        //    (mappedObject as IEntity)?.AssignDefaultValuesToComplexPropertiesIfNull();

        //    return mappedObject;
        //}

        //public TDestination MapDirect<TDestination>(object source)
        //{
        //    var mappedObject = (TDestination) FormatterServices.GetUninitializedObject(typeof(TDestination));
        //    (mappedObject as IEntity)?.AssignDefaultValuesToComplexPropertiesIfNull();

        //    return mappedObject;
        //}

        public static void AddMaps(IEnumerable<MapSetting> maps = null)
        {
            AddMapsInternal(maps);
        }

        protected static void AddMapsInternal(IEnumerable<MapSetting> maps)
        {
            if (maps == null)
                return;

            var mapList = maps.ToList();
            foreach (var map in mapList)
            {
                if (MapSettings.Any(x => (x.Source == map.Source) && (x.Destination == map.Destination)))
                    continue;

                MapSettings.Add(map);
            }
        }

        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMissingTypeMaps = true;

                foreach (var map in MapSettings)
                    RegisterMap(config, map.Source, map.Destination, map.CustomMap);
            });
        }

        public static void AddMaps(params MapSetting[] maps)
        {
            AddMapsInternal(maps);
        }

        protected static IMappingExpression RegisterMap(IMapperConfigurationExpression config,
            Type source, Type destination, Func<IMappingExpression, IMappingExpression> customMapping = null)
        {
            var expression = config.CreateMap(source, destination);

            if (customMapping != null)
                expression = customMapping(expression);

            return expression;
        }
    }
}