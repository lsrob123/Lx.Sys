using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.ServiceBus;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public abstract class ResponseBase : ResultBase, IResponse, IBusEvent
    {
    }
}