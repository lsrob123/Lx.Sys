using Lx.Utilities.Modelling.DTOs;
using Lx.Utilities.Modelling.Processes;
using Lx.Utilities.Services.ServiceBus.Nsb;
using NServiceBus;

namespace Lx.Utilities.Modelling.NSB {
    public class RequestHandlers : RequestHandlersBase, IHandleMessages<TrialRequest> {
        private readonly ITrialService _trialService;

        public RequestHandlers(IBus bus, ITrialService trialService) : base(bus) {
            _trialService = trialService;
        }

        public void Handle(TrialRequest message) {
            var response = _trialService.Process(message);
            PublishToBus(response);
        }
    }
}