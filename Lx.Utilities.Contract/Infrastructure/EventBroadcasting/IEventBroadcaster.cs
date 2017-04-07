using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Infrastructure.EventBroadcasting {
    public interface IEventBroadcaster {
        void Broadcast<TEvent>(TEvent e) where TEvent : ResponseBase;
    }
}