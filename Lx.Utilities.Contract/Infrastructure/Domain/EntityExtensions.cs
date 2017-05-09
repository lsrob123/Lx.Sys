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

        public static TEntity WithValidKey<TEntity>(this TEntity entity)
            where TEntity : IEntity {
            entity.EnsureValidKey();
            return entity;
        }

        public static TEntity WithTimeCreated<TEntity>(this TEntity entity, DateTimeOffset? timeCreated = null)
            where TEntity : IEntity {
            entity.SetTimeCreated(timeCreated ?? DateTimeOffset.UtcNow);
            return entity;
        }

        public static TEntity WithTimeModified<TEntity>(this TEntity entity, DateTimeOffset? timeModified = null)
            where TEntity : IEntity {
            entity.SetTimeModified(timeModified ?? DateTimeOffset.UtcNow);
            return entity;
        }
    }
}