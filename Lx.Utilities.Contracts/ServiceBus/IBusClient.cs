namespace Lx.Utilities.Contracts.ServiceBus
{
    public interface IBusClient
    {
        //string EndpointName { get; }

        void Send<T>(T busCommand, string endpointNameOrQueueName = null)
            where T : IBusCommand;

        void Publish<T>(T busEvent)
            where T : IBusMessage;
    }
}