using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Dto;

namespace Lx.Utilities.Services.ServiceBus {
    public abstract class MediatedResponseHandlersBase {
        protected readonly IMediator Mediator;

        protected MediatedResponseHandlersBase(IMediator mediator) {
            Mediator = mediator;
        }

        protected void Publish<TResponse>(TResponse response)
            where TResponse : IResponse {
            Mediator.Publish(response);
        }
    }
}