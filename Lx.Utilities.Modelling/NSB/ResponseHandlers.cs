using Lx.Utilities.Contract.Mediator;
using Lx.Utilities.Modelling.DTOs;
using Lx.Utilities.Services.ServiceBus;
using NServiceBus;

namespace Lx.Utilities.Modelling.NSB {
    public class ResponseHandlers : ResponseHandlersBase, IHandleMessages<TrialResponse> {
        public ResponseHandlers(IMediator mediator) : base(mediator) {}

        public void Handle(TrialResponse message) {
            Publish(message);
        }
    }
}