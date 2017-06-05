using System;
using Lx.Membership.Contracts.DTOs;
using Lx.Membership.Contracts.Events;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Membership.Persistence.UowFactories
{
    public interface IMembershipUowFactory
    {
        MemberUpdatedEvent CreateOrUpdateMember(IBasicRequestKey request, MemberUpdateDto dto,
            Func<MemberUpdateDto, UserProfileDto> createUserProfileDto);
    }
}