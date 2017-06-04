using System;
using Lx.Membership.Persistence.EF;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Services.Persistence.EF;

namespace Lx.Membership.Persistence.Uow
{
    public class MembershipUow : DbContextUnitOfWork<MembershipDbContext>
    {
        public MembershipUow(Func<MembershipDbContext> contextFactory, ICacheFactory cacheFactory,
            IMappingService mappingService, ILogger logger) 
            : base(contextFactory, cacheFactory, mappingService, logger)
        {
        }
    }
}