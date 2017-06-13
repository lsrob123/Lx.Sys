using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb
{
    public abstract class RequestHandlersBase
    {
        protected readonly IBus Bus;

        protected RequestHandlersBase(IBus bus)
        {
            Bus = bus;
        }

        protected virtual void PublishToBus(IResponse response)
        {
            Bus.Publish(response);
        }
    }
}