using Lx.Utilities.Contracts.ServiceBus;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public abstract class EventBase : ResultBase, IBusEvent
    {
    }
}