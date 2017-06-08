using System;
using Lx.Membership.Contracts.DTOs;
using Lx.Membership.Contracts.Events;
using Lx.Membership.Contracts.RequestsResponses;
using Lx.Membership.Persistence.UowFactories;
using Lx.Shared.All.Domains.Identity.Config;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Contract.Mapping;
using Lx.Utilities.Contract.Membership.DTOs;
using Lx.Utilities.Contract.Serialization;

namespace Lx.Membership.Services.Processes
{
    public class MemberService : IMemberService
    {
        private readonly IMappingService _mappingService;
        private readonly IMembershipUowFactory _membershipUowFactory;
        private readonly ISerializer _serializer;
        private readonly IUserProfileConfig _userProfileConfig;

        public MemberService(IMembershipUowFactory membershipUowFactory, IMappingService mappingService,
            ISerializer serializer, IUserProfileConfig userProfileConfig)
        {
            _membershipUowFactory = membershipUowFactory;
            _mappingService = mappingService;
            _serializer = serializer;
            _userProfileConfig = userProfileConfig;
        }

        public MemberUpdatedEvent CreateOrUpdateMember(UpdateMemberRequestBase request)
        {
            var updatedEvent =
                _membershipUowFactory.CreateOrUpdateMember(request, request.Member, CreateUserProfileDto);
            return updatedEvent;
        }

        private UserProfileDto CreateUserProfileDto(MemberUpdateDto memberUpdateDto)
        {
            var info = _mappingService.Map<BasicMemberInfo>(memberUpdateDto);
            var profile = new UserProfileDto
            {
                Body = _serializer.Serialize(info),
                Key = Guid.NewGuid(),
                UserKey = memberUpdateDto.Key,
                UserProfileOriginator = _userProfileConfig.UserProfileOriginator
            };

            return profile;
        }
    }
}