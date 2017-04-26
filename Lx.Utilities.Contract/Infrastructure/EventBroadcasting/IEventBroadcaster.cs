using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Enums;

namespace Lx.Utilities.Contract.Infrastructure.EventBroadcasting
{
    public interface IEventBroadcaster
    {
        EventBroadcastingScope AllowedScope { get; }
        void Broadcast<TEvent>(TEvent e) where TEvent : ResultBase;
    }
}