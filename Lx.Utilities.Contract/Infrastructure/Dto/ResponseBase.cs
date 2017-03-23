using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.Dto {
    public abstract class ResponseBase : ResultBase, IResponse, IBusEvent {}
}