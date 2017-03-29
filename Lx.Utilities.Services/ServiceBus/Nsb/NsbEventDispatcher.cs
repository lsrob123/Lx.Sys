using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.EventDispacthing;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb {
    public class NsbEventDispatcher : IEventDispatcher {
        protected readonly IBus Bus;

        public NsbEventDispatcher(IBus bus) {
            Bus = bus;
        }

        public void Dispatch<TEvent>(TEvent e) where TEvent : ResponseBase {
            Bus.Publish(e);
        }
    }
}