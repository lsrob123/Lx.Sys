using System.Collections.Generic;
using Lx.Identity.Services.Processes;
using Lx.Shared.All.Domains.Identity.Config;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Contracts.Email;
using Lx.Utilities.Contracts.Infrastructure.Extensions;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Identity.Nsb.Users
{
    public class RequestHandlers : RequestHandlersBase, IHandleMessages<CreateUserRequest>,
        IHandleMessages<ResetPasswordRequest>
    {
        private readonly IUserService _userService;
        private readonly IVerificationService _verificationService;
        private readonly ICommonBusEndpointSettings _commonBusEndpointSettings;

        public RequestHandlers(IBus bus, IUserService userService, IVerificationService verificationService, ICommonBusEndpointSettings commonBusEndpointSettings) : base(bus)
        {
            _userService = userService;
            _verificationService = verificationService;
            _commonBusEndpointSettings = commonBusEndpointSettings;
        }

        public void Handle(CreateUserRequest message)
        {
            var response = _userService.CreateUser(message);
            PublishToBus(response);
        }

        public void Handle(ResetPasswordRequest message)
        {
            var verificationCodeCreatedEvent = _verificationService.StartResetPassword(message);
            PublishToBus(verificationCodeCreatedEvent);
        }
    }
}