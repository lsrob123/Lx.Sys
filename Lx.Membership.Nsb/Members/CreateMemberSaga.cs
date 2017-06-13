using System;
using Lx.Membership.Contracts.Events;
using Lx.Membership.Contracts.RequestsResponses;
using Lx.Membership.Services.Processes;
using Lx.Shared.All.Domains.Identity.Config;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Shared.All.Domains.Identity.Enumerations;
using Lx.Utilities.Contracts.Caching;
using Lx.Utilities.Contracts.Infrastructure.Extensions;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mapping;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;
using NServiceBus.Saga;

namespace Lx.Membership.Nsb.Members
{
    public class CreateMemberSaga : SagaBase<CreateMemberSagaData>, IAmStartedByMessages<CreateMemberRequest>,
        IHandleMessages<CreateUserResponse>
    {
        private readonly ICommonBusEndpointSettings _commonBusEndpointSettings;
        private readonly IMemberService _memberService;

        public CreateMemberSaga(ICacheFactory cacheFactory, ISerializer serializer, IMappingService mappingService,
            ILogger logger, IMemberService memberService, ICommonBusEndpointSettings commonBusEndpointSettings) : base(
            cacheFactory, serializer, mappingService, logger)
        {
            _memberService = memberService;
            _commonBusEndpointSettings = commonBusEndpointSettings;
        }

        public void Handle(CreateMemberRequest message)
        {
            if (string.IsNullOrWhiteSpace(message.Member.Email?.Address))
            {
                Bus.Publish(new MemberUpdatedEvent().LinkTo(message)
                    .WithProcessResult(new ArgumentException("Email address is required"),
                        "Email address is required."));
                MarkAsComplete();
            }

            Data.Email = message.Member.Email?.Address;
            DataCache.SetItem(message, Data.Email);
            var createUserRequest = new CreateUserRequest
            {
                UserUpdate = MappingService.Map<UserUpdateDto>(message.Member)
            }.LinkTo(message);

            Bus.Send(_commonBusEndpointSettings.Identity, createUserRequest);
        }

        public void Handle(CreateUserResponse message)
        {
            if (!message.ResultType.Equals(UserUpdateResultType.Created))
            {
                Bus.Publish(new MemberUpdatedEvent().LinkTo(message)
                    .WithProcessResult(new Exception(message.ResultType.Name), message.ResultType.Name));
                MarkAsComplete();
            }

            var createMemberRequest = DataCache.GetItem<CreateMemberRequest>(Data.Email);
            _memberService.CreateOrUpdateMember(createMemberRequest);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreateMemberSagaData> mapper)
        {
            mapper.ConfigureMapping<CreateUserResponse>(m => m.User.Email.Address)
                .ToSaga(sd => sd.Email);
        }
    }
}