using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Enums;
using Lx.Utilities.Contract.Infrastructure.EventBroadcasting;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb
{
    public class NsbEventBroadcaster : IEventBroadcaster
    {
        protected readonly IBus Bus;

        public NsbEventBroadcaster(IBus bus)
        {
            Bus = bus;
        }

        public EventBroadcastingScope AllowedScope => EventBroadcastingScope.CrossProcesses;

        public void Broadcast<TEvent>(TEvent e) where TEvent : ResponseBase
        {
            Bus.Publish(e);
        }
    }
}