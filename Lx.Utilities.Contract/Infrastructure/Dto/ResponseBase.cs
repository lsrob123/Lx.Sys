using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.DTO {
    public abstract class ResponseBase : ResultBase, IResponse, IBusEvent {}
}