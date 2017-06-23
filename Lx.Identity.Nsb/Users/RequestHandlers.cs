using Lx.Identity.Services.Processes;
using Lx.Shared.All.Domains.Identity.RequestsResponses;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Identity.Nsb.Users
{
    public class RequestHandlers : RequestHandlersBase, IHandleMessages<CreateUserRequest>,
        IHandleMessages<CreatePasswordResetVerificationCodeRequest>, IHandleMessages<ResetPasswordRequest>
    {
        private readonly IUserService _userService;
        private readonly IVerificationService _verificationService;

        public RequestHandlers(IBus bus, IUserService userService, IVerificationService verificationService) : base(bus)
        {
            _userService = userService;
            _verificationService = verificationService;
        }

        public void Handle(CreatePasswordResetVerificationCodeRequest message)
        {
            var verificationCodeCreatedEvent = _verificationService.CreatePasswordResetVerificationCode(message);
            PublishToBus(verificationCodeCreatedEvent);
        }

        public void Handle(CreateUserRequest message)
        {
            var response = _userService.CreateUser(message);
            PublishToBus(response);
        }

        public void Handle(ResetPasswordRequest message)
        {
            var response = _verificationService.ResetPassword(message);
            PublishToBus(response);
        }
    }
}