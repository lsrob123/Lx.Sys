using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Enums;

namespace Lx.Utilities.Contract.Infrastructure.EventBroadcasting {
    public interface IEventBroadcastingProxy {
        void Register<TBroadcaster>(TBroadcaster broadcaster) where TBroadcaster : IEventBroadcaster;
        void Broadcast<TEvent>(TEvent e, EventBroadcastingScope scopes) where TEvent : ResultBase;
    }
}