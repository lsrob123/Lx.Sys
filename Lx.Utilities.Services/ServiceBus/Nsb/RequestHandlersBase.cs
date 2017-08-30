using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.ServiceBus;
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

        protected virtual void PublishToBus<TResult>(TResult response) where TResult : IResultBase, IBusEvent
        {
            Bus.Publish(response);
        }

        protected virtual void SendToBusEndpoint<TRequest>(TRequest request, string busEndpoint)
            where TRequest : RequestBase
        {
            Bus.Send(busEndpoint, request);
        }
    }
}