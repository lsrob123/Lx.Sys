using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Enums;
using Lx.Utilities.Contracts.Infrastructure.EventBroadcasting;
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

        public void Broadcast<TEvent>(TEvent e) where TEvent : ResultBase
        {
            Bus.Publish(e);
        }
    }
}