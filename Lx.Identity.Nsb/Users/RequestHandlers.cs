using Lx.Identity.Services.Services;
using Lx.Shared.All.Domains.Identity.DTOs;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Identity.Nsb.Users
{
    public class RequestHandlers : RequestHandlersBase, IHandleMessages<CreateUserRequest>
    {
        private readonly IUserService _userService;

        public RequestHandlers(IBus bus, IUserService userService) : base(bus)
        {
            _userService = userService;
        }

        public void Handle(CreateUserRequest message)
        {
            var response = _userService.CreateUser(message);
            PublishToBus(response);
        }
    }
}