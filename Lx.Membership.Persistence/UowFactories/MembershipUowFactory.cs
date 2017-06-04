using System.Collections.Generic;
using System.Linq;
using Lx.Membership.Contracts.DTOs;
using Lx.Membership.Contracts.Events;
using Lx.Membership.Domain.Entities;
using Lx.Membership.Persistence.EF;
using Lx.Membership.Persistence.Uow;
using Lx.Utilities.Contract.Caching;
using Lx.Utilities.Contract.Infrastructure.Domain;
using Lx.Utilities.Contract.Infrastructure.EventBroadcasting;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Membership.DTOs;
using Lx.Utilities.Contract.Membership.Entities;
using Lx.Utilities.Contract.Membership.Enumerations;
using Lx.Utilities.Contract.Persistence;
using Lx.Utilities.Contract.Serialization;
using Lx.Utilities.Services.Persistence;

namespace Lx.Membership.Persistence.UowFactories
{
    public class MembershipUowFactory : UnitOfWorkFactoryBase<MembershipUow>
    {
        public MembershipUowFactory(IDbConfig primaryDbConfig, ILogger logger, ICacheFactory cacheFactory,
            IMappingService mappingService, ISerializer serializer, IEventBroadcastingProxy eventDispatchingProxy) :
            base(primaryDbConfig, logger, cacheFactory, mappingService, serializer, eventDispatchingProxy)
        {
        }

        protected override MembershipUow GetUnitOfWork()
        {
            return new MembershipUow(() => new MembershipDbContext(PrimaryDbConfig.ConnectionString), CacheFactory,
                MappingService, Logger);
        }

        public MemberUpdatedEvent CreateMember(IBasicRequestKey request, MemberUpdateDto dto)
        {
            var member = MappingService.Map<Member>(dto).AsNewEntity();

            if (dto.Roles == null)
                dto.Roles = new List<RoleDto>();
            if (!dto.Roles.Any())
                dto.Roles.Add(new RoleDto {RoleType = RoleType.BasicMember});
            foreach (var roleDto in dto.Roles)
                roleDto.UserKey = member.Key;

            var roles = dto.Roles.Select(x => MappingService.Map<Role>(x).AsNewEntity()).ToList();

            var updatedEvent = new MemberUpdatedEvent().LinkTo(request);
            updatedEvent.WithProcessResult(ExecuteWithProcessResult(uow =>
            {
                uow.Store.Add(member);
                foreach (var role in roles)
                    uow.Store.Add(role);
            }));
            return updatedEvent;
        }
    }
}