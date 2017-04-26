using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.DTOs
{
    public abstract class EventBase : ResultBase, IBusEvent
    {
    }
}