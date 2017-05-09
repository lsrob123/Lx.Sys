using System;

namespace Lx.Utilities.Contract.Infrastructure.Domain {
    public static class EntityExtensions {
        public static TEntity WithKey<TEntity>(this TEntity entity, Guid key)
            where TEntity : IEntity {
            entity.SetKey(key);
            return entity;
        }

        public static TEntity WithId<TEntity>(this TEntity entity, long id)
            where TEntity : IEntity {
            entity.SetId(id);
            return entity;
        }

        public static TEntity WithTimeCreated<TEntity>(this TEntity entity, DateTimeOffset? timeCreated)
            where TEntity : IEntity {
            entity.SetTimeCreated(timeCreated);
            return entity;
        }

        public static TEntity WithTimeModified<TEntity>(this TEntity entity, DateTimeOffset? timeModified)
            where TEntity : IEntity {
            entity.SetTimeModified(timeModified);
            return entity;
        }

        public static TEntity AsNewEntity<TEntity>(this TEntity entity)
            where TEntity : IEntity {
            if (entity.Key == Guid.Empty)
                entity.EnsureValidKey();

            return entity.WithTimeCreated(entity.TimeCreated ?? DateTimeOffset.UtcNow)
                .WithTimeModified(entity.TimeModified ?? DateTimeOffset.UtcNow);
        }
    }
}