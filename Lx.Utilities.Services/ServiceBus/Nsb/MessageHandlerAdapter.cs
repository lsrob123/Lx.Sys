using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Mediator;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb
{
    public class MessageHandlerAdapter<TMessage> : IHandleMessages<TMessage>
        where TMessage : IResponse
    {
        public void Handle(TMessage message)
        {
            Mediator.Default.Publish(message);
        }
    }
}