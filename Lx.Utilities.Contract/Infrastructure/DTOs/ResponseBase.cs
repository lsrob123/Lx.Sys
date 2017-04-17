using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.DTOs
{
    public abstract class ResponseBase : ResultBase, IResponse, IBusEvent
    {
    }
}