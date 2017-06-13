using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Modelling.DTOs;
using Lx.Utilities.Services.ServiceBus;
using NServiceBus;

namespace Lx.Utilities.Modelling.NSB
{
    /// <summary>
    ///     It might not be needed after having MessageHandlerAdapterLister
    /// </summary>
    public class ResponseHandlers : ResponseHandlersBase, IHandleMessages<TrialResponse>
    {
        public ResponseHandlers(IMediator mediator) : base(mediator)
        {
        }

        public void Handle(TrialResponse message)
        {
            Publish(message);
        }
    }
}