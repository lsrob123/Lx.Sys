using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Mediator;

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