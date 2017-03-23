namespace Lx.Utilities.Contract.Mapping {
    public interface IMappingService {
        TDestination Map<TDestination>(object source);

        TDestination Map<TSource, TDestination>(TSource source);

        //object Map(object source, Type destinationType);

        //IRelationalModel<TEntity> Map<TEntity>(TEntity entity)
        //    where TEntity : class, IEntity;

        //Type GetRelationalModelType(Type entityType);
    }
}