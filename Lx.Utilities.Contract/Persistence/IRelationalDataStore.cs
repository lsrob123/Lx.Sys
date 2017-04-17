using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Enums;

namespace Lx.Utilities.Contract.Persistence
{
    public interface IRelationalDataStore
    {
        ICollection<TWithRelationalId> List<TWithRelationalId>(
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> queryFunc)
            where TWithRelationalId : class, IWithRelationalId, new();

        long Count<TWithRelationalId>(
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> queryFunc)
            where TWithRelationalId : class, IWithRelationalId, new();

        TWithRelationalId FirstOrDefault<TWithRelationalId>(
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> queryFunc)
            where TWithRelationalId : class, IWithRelationalId, new();

        TWithRelationalId FirstOrDefault<TWithRelationalId>(Expression<Func<TWithRelationalId, bool>> queryExpression,
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> preQueryFunc = null)
            where TWithRelationalId : class, IWithRelationalId, new();

        TWithRelationalId SingleOrDefault<TWithRelationalId>(Expression<Func<TWithRelationalId, bool>> queryExpression,
            Func<IQueryable<TWithRelationalId>, IQueryable<TWithRelationalId>> preQueryFunc = null)
            where TWithRelationalId : class, IWithRelationalId, new();

        TWithRelationalId AddOrUpdate<TWithRelationalId>(TWithRelationalId withRelationalId,
            Expression<Func<TWithRelationalId, bool>> queryExpression, bool addWithoutRelatedEntities = false,
            Action<TWithRelationalId> updatePropertiesAction = null,
            AddUpdateOptions addUpdateOptions = AddUpdateOptions.Add | AddUpdateOptions.Update, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId, new();

        TEntity AddOrUpdateByKey<TEntity>(TEntity entity, bool addWithoutRelatedEntities = false,
            Action<TEntity> updatePropertiesAction = null,
            AddUpdateOptions addUpdateOptions = AddUpdateOptions.Add | AddUpdateOptions.Update, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        ProcessResult Delete<TWithRelationalId>(Func<TWithRelationalId, bool> queryExpression, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId, new();

        ProcessResult DeleteByKey<TEntity>(Guid key, bool saveChanges = true)
            where TEntity : class, IEntity, new();

        IReadOnlyCollection<TWithRelationalId> UpdatePropertiesOnly<TWithRelationalId>(
            Expression<Func<TWithRelationalId, bool>> searchExpression,
            Action<TWithRelationalId> updateAction,
            bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId;

        ProcessResult Save<TWithRelationalId>(TWithRelationalId existing, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId;

        TWithRelationalId Add<TWithRelationalId>(TWithRelationalId withRelationalId,
            bool addWithoutRelatedEntities = false, bool saveChanges = true)
            where TWithRelationalId : class, IWithRelationalId, new();

        void NotifyModified<TWithRelationalId>(TWithRelationalId item)
            where TWithRelationalId : class, IWithRelationalId;

        void Detach<TWithRelationalId>(TWithRelationalId withRelationalId)
            where TWithRelationalId : class, IWithRelationalId, new();

        void Attach<TWithRelationalId>(TWithRelationalId withRelationalId)
            where TWithRelationalId : class, IWithRelationalId, new();
    }
}