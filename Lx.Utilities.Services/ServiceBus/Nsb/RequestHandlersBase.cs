using Lx.Utilities.Contract.Infrastructure.DTO;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.NSB {
    public abstract class RequestHandlersBase {
        protected readonly IBus Bus;

        protected RequestHandlersBase(IBus bus) {
            Bus = bus;
        }

        protected virtual void PublishToBus(IResponse response) {
            Bus.Publish(response);
        }
    }
}