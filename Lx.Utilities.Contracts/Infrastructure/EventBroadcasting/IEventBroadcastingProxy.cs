using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Enums;

namespace Lx.Utilities.Contracts.Infrastructure.EventBroadcasting
{
    public interface IEventBroadcastingProxy
    {
        void Register<TBroadcaster>(TBroadcaster broadcaster) where TBroadcaster : IEventBroadcaster;
        void Broadcast<TEvent>(TEvent e, EventBroadcastingScope scopes) where TEvent : ResultBase;
    }
}