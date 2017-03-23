using System;
using System.Data.Entity;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;

namespace Lx.Utilities.Services.Persistence.EF {
    public class DbContextUnitOfWork<TDbContext> : UnitOfWorkBase
        where TDbContext : DbContext {
        protected TDbContext DbContext;

        public DbContextUnitOfWork(Func<TDbContext> contextFactory, ICacheFactory cacheFactory,
            IMappingService mappingService, ILogger logger) {
            Cache = cacheFactory.NewDisposableCache();
            DbContext = contextFactory();
            MappingService = mappingService;
            Store = new DataStore<TDbContext>(DbContext, MappingService, logger);
        }

        public ICacheWithHashes Cache { get; protected set; }
        public DataStore<TDbContext> Store { get; protected set; }
        public IMappingService MappingService { get; protected set; }

        protected override void DisposingAction() {
            DbContext.Dispose();
            Cache.Dispose();
        }

        public override void SaveChanges() {
            DbContext.SaveChanges();
        }
    }
}