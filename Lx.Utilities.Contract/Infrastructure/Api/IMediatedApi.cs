using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.ServiceBus;

namespace Lx.Utilities.Contract.Infrastructure.Api
{
    /// <summary>
    ///     IMediatedApi
    /// </summary>
    public interface IMediatedApi
    {
        /// <summary>
        ///     Send request to bus to start some process.
        /// </summary>
        /// <typeparam name="TBusCommand">Request type which implements IBusCommand</typeparam>
        /// <param name="message">Request instance</param>
        /// <param name="endpointName">
        ///     Bus endpoint name. Default is null, which means the endpoint name from injected bus settings
        ///     should be used
        /// </param>
        void SendToBus<TBusCommand>(TBusCommand message, string endpointName = null)
            where TBusCommand : IBusCommand;

        /// <summary>
        ///     Publish a message to the bus.
        /// </summary>
        /// <remarks>
        ///     Caution: It is supposed to be called by ASP.NET Web API or alike to accept notification issued by third parties,
        ///     eg. instant payment notifications from PayPal.
        /// </remarks>
        /// <typeparam name="TBusMessage">Message type which implements IBusMessage</typeparam>
        /// <param name="message">Message instance</param>
        void PublishToBus<TBusMessage>(TBusMessage message)
            where TBusMessage : IBusMessage;

        /// <summary>
        ///     Publish process response back to the front end, typically SignalR hubs, via mediator.
        /// </summary>
        /// <typeparam name="TMessage">Message type which implements IMessageBase</typeparam>
        /// <param name="message">Message instance</param>
        void PublishToFrontEnd<TMessage>(TMessage message) where TMessage : IMessageBase;
    }
}