using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public abstract class ReplyBase : ResultBase, IResponse, IBusMessage {}
}