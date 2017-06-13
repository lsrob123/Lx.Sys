using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Infrastructure.Enums;

namespace Lx.Utilities.Contracts.Infrastructure.EventBroadcasting
{
    public interface IEventBroadcaster
    {
        EventBroadcastingScope AllowedScope { get; }
        void Broadcast<TEvent>(TEvent e) where TEvent : ResultBase;
    }
}