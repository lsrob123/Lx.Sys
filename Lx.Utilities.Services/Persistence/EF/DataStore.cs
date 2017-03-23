using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Enums;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Persistence;
using Lx.Utilities.Services.Infrastructure;

namespace Lx.Utilities.Services.Persistence.EF {
    public class DataStore<TDbContext> : IRelationalDataStore where TDbContext : DbContext {
        protected readonly TDbContext DbContext;
        protected readonly ILogger Logger;
        protected readonly IMappingService MappingService;

        public DataStore(TDbContext dbContext, IMappingService mappingService, ILogger logger) {
            DbContext = dbContext;
            MappingService = mappingService;
            Logger = logger;
        }

        public ICollection<TWithRelationalId> List<TWithRelationalId>(
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> queryFunc)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var dbSet = DbContext.Set<TWithRelationalId>();

            var entities = new List<TWithRelationalId>(queryFunc == null ? dbSet : queryFunc(dbSet));

            return entities;
        }

        public long Count<TWithRelationalId>(
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> queryFunc)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var dbSet = DbContext.Set<TWithRelationalId>();

            var count = queryFunc?.Invoke(dbSet).LongCount() ?? dbSet.LongCount();

            return count;
        }

        public TWithRelationalId FirstOrDefault<TWithRelationalId>(
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> queryFunc)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var record = queryFunc(DbContext.Set<TWithRelationalId>()).FirstOrDefault();
            return record;
        }

        public TWithRelationalId FirstOrDefault<TWithRelationalId>(
            Expression<Func<TWithRelationalId, bool>> queryExpression,
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> preQueryFunc = null)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var dbSet = DbContext.Set<TWithRelationalId>();
            var entity = preQueryFunc == null
                ? dbSet.FirstOrDefault(queryExpression)
                : preQueryFunc(dbSet).FirstOrDefault(queryExpression);

            return entity;
        }

        public TWithRelationalId SingleOrDefault<TWithRelationalId>(
            Expression<Func<TWithRelationalId, bool>> queryExpression,
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> preQueryFunc = null)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var dbSet = DbContext.Set<TWithRelationalId>();
            var entity = preQueryFunc == null
                ? dbSet.SingleOrDefault(queryExpression)
                : preQueryFunc(dbSet).SingleOrDefault(queryExpression);
            return entity;
        }

        public TWithRelationalId AddOrUpdate<TWithRelationalId>(TWithRelationalId withRelationalId,
            Expression<Func<TWithRelationalId, bool>> queryExpression, bool addWithoutRelatedEntities = false,
            Action<TWithRelationalId> updatePropertiesAction = null,
            AddUpdateOptions addUpdateOptions = AddUpdateOptions.Add | AddUpdateOptions.Update, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId, new() {
            TWithRelationalId addedOrUpdatedItem = null;

            var dbSet = DbContext.Set<TWithRelationalId>();
            var existingItems = dbSet.Where(queryExpression).ToList();

            if (!existingItems.Any()) {
                if (!addUpdateOptions.HasFlag(AddUpdateOptions.Add))
                    return null;

                if (addWithoutRelatedEntities) {
                    dbSet.Attach(withRelationalId);
                    DbContext.Entry(withRelationalId).State = EntityState.Added;
                } else {
                    dbSet.Add(withRelationalId);
                }
                addedOrUpdatedItem = withRelationalId;
            } else if (updatePropertiesAction == null) {
                var firstMatch = existingItems.First();
                withRelationalId.SetId(firstMatch.Id);

                if (firstMatch.TimeCreated.HasValue)
                    withRelationalId.SetTimeCreated(firstMatch.TimeCreated);

                DbContext.Entry(firstMatch).State = EntityState.Detached;

                dbSet.Attach(withRelationalId);
                DbContext.Entry(withRelationalId).State = EntityState.Modified;
                addedOrUpdatedItem = withRelationalId;
            } else {
                foreach (var item in existingItems) {
                    updatePropertiesAction(item);
                    DbContext.Entry(item).State = EntityState.Modified;
                    addedOrUpdatedItem = addedOrUpdatedItem ?? item;
                }
            }

            if (saveChanges)
                DbContext.SaveChanges();

            return addedOrUpdatedItem;
        }

        public TEntity AddOrUpdateByKey<TEntity>(TEntity entity, bool addWithoutRelatedEntities = false,
            Action<TEntity> updatePropertiesAction = null,
            AddUpdateOptions addUpdateOptions = AddUpdateOptions.Add | AddUpdateOptions.Update, bool saveChanges = true)
            where TEntity : class, IEntity, new() {
            if (entity.Key == Guid.Empty)
                return null;

            return AddOrUpdate(entity, existing => existing.Key == entity.Key, addWithoutRelatedEntities,
                x => updatePropertiesAction?.Invoke(x), addUpdateOptions, saveChanges);
        }

        public ProcessResult Delete<TWithRelationalId>(Func<TWithRelationalId, bool> queryExpression,
            bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var dbSet = DbContext.Set<TWithRelationalId>();
            var existing = dbSet.SingleOrDefault(x => queryExpression(x));
            if (existing != null)
                DbContext.Entry(existing).State = EntityState.Deleted;

            if (saveChanges)
                DbContext.SaveChanges();

            return ProcessResultType.Ok;
        }

        public ProcessResult DeleteByKey<TEntity>(Guid key, bool saveChanges = true)
            where TEntity : class, IEntity, new() {
            return Delete<TEntity>(existing => existing.Key == key, saveChanges);
        }

        public IReadOnlyCollection<TWithRelationalId> UpdatePropertiesOnly<TWithRelationalId>(
            Expression<Func<TWithRelationalId, bool>> searchExpression,
            Action<TWithRelationalId> updateAction,
            bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId {
            if (updateAction == null) {
                new ArgumentNullException(nameof(updateAction)).WriteToLog(Logger);
                return null;
            }

            var dbSet = DbContext.Set<TWithRelationalId>();
            var existingItems = dbSet.Where(searchExpression).ToList();

            if (!existingItems.Any())
                return null;

            foreach (var item in existingItems) {
                updateAction(item);
                DbContext.Entry(item).State = EntityState.Modified;
            }

            if (saveChanges)
                DbContext.SaveChanges();

            return existingItems;
        }

        public ProcessResult Save<TWithRelationalId>(TWithRelationalId existing, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId {
            NotifyModified(existing);

            if (saveChanges)
                DbContext.SaveChanges();

            return ProcessResultType.Ok;
        }

        public void NotifyModified<TWithRelationalId>(TWithRelationalId item)
            where TWithRelationalId : class, IWithRelationalId {
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public TWithRelationalId Add<TWithRelationalId>(TWithRelationalId withRelationalId,
            bool addWithoutRelatedEntities = false, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId, new() {
            var dbSet = DbContext.Set<TWithRelationalId>();
            if (addWithoutRelatedEntities) {
                dbSet.Attach(withRelationalId);
                DbContext.Entry(withRelationalId).State = EntityState.Added;
            } else {
                dbSet.Add(withRelationalId);
            }

            if (saveChanges)
                DbContext.SaveChanges();

            return withRelationalId;
        }

        public virtual void Detach<TWithRelationalId>(TWithRelationalId withRelationalId)
            where TWithRelationalId : class, IWithRelationalId, new() {
            DbContext.Entry(withRelationalId).State = EntityState.Detached;
        }

        public virtual void Attach<TWithRelationalId>(TWithRelationalId withRelationalId)
            where TWithRelationalId : class, IWithRelationalId, new() {
            DbContext.Set<TWithRelationalId>().Attach(withRelationalId);
        }

        public IQueryable<TWithRelationalId> DbSetAsQueryable<TWithRelationalId>()
            where TWithRelationalId : class, IWithRelationalId, new() {
            return DbContext.Set<TWithRelationalId>().AsQueryable();
        }

        public DbSet DbSet<TWithRelationalId>() where TWithRelationalId : class, IWithRelationalId, new() {
            return DbContext.Set<TWithRelationalId>();
        }
    }
}