using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Mediator;

namespace Lx.Utilities.Services.ServiceBus
{
    public abstract class ResponseHandlersBase
    {
        protected readonly IMediator Mediator;

        protected ResponseHandlersBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected virtual void PublishByMediator<TResponse>(TResponse response)
            where TResponse : IResultBase
        {
            Mediator.Publish(response);
        }
    }
}