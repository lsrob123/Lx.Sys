using System;
using System.Collections.Generic;
using System.Linq;
using Lx.Membership.Contracts.DTOs;
using Lx.Membership.Contracts.Events;
using Lx.Membership.Domain.Entities;
using Lx.Membership.Persistence.EF;
using Lx.Membership.Persistence.Uow;
using Lx.Shared.All.Domains.Identity.DTOs;
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
    public class MembershipUowFactory : UnitOfWorkFactoryBase<MembershipUow>, IMembershipUowFactory
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

        public MemberUpdatedEvent CreateOrUpdateMember(IBasicRequestKey request, MemberUpdateDto dto,
            Func<MemberUpdateDto, UserProfileDto> createUserProfileDto)
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
                var updatedMember = uow.Store.AddOrUpdate(member,
                    x => x.Email.Address == member.Email.Address ||
                         x.Mobile.LocalNumberWithAreaCodeInDigits == member.Mobile.LocalNumberWithAreaCodeInDigits ||
                         x.Username == member.Username
                );
                var updatedMemberDto = MappingService.Map<MemberUpdateDto>(updatedMember);
                updatedMemberDto.Roles = new List<RoleDto>();
                uow.Store.Delete<Role>(x => x.UserKey == updatedMember.Key);
                foreach (var role in roles)
                {
                    var updatedRole = uow.Store.Add(role);
                    updatedMemberDto.Roles.Add(MappingService.Map<RoleDto>(updatedRole));
                }
                updatedEvent.WithMember(updatedMemberDto)
                    .WithUserProfile(createUserProfileDto?.Invoke(updatedMemberDto));
            }));
            return updatedEvent;
        }
    }
}