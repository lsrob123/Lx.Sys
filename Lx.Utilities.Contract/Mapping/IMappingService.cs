namespace Lx.Utilities.Contract.Mapping {
    public interface IMappingService {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source);

        //IRelationalModel<TEntity> Map<TEntity>(TEntity entity)

        //object Map(object source, Type destinationType);
        //    where TEntity : class, IEntity;

        //Type GetRelationalModelType(Type entityType);
    }
}