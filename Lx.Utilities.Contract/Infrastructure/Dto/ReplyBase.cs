using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public abstract class ReplyBase : ResultBase, IResponse, IBusMessage {}
}