using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.DTO;

namespace Lx.Utilities.Services.ServiceBus {
    public abstract class ResponseHandlersBase {
        protected readonly IMediator Mediator;

        protected ResponseHandlersBase(IMediator mediator) {
            Mediator = mediator;
        }

        protected virtual void Publish<TResponse>(TResponse response)
            where TResponse : IResponse {
            Mediator.Publish(response);
        }
    }
}