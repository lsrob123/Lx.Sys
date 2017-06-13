using System;
using Lx.Identity.Persistence.EF;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Services.Persistence.EF;

namespace Lx.Identity.Persistence.Uow
{
    public class IdentityUow : DbContextUnitOfWork<IdentityDbContext>
    {
        public IdentityUow(Func<IdentityDbContext> contextFactory, ICacheFactory cacheFactory,
            IMappingService mappingService, ILogger logger) : base(contextFactory, cacheFactory, mappingService, logger)
        {
        }
    }
}