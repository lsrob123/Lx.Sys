using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Dto;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb {
    public class GeneralDataResponseHandler : IHandleMessages<GeneralDataResponse> {
        protected readonly IMediator Mediator;

        public GeneralDataResponseHandler(IMediator mediator) {
            Mediator = mediator;
        }

        public void Handle(GeneralDataResponse message) {
            Mediator.Publish(message);
        }
    }
}