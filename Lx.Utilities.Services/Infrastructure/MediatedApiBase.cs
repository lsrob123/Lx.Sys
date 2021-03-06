﻿using Lx.Utilities.Contracts.Infrastructure.Api;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Mediator;
using Lx.Utilities.Contracts.Serialization;
using Lx.Utilities.Contracts.ServiceBus;
using NServiceBus;

namespace Lx.Utilities.Services.Infrastructure
{
    /// <summary>
    ///     Base class for mediated API services
    /// </summary>
    public abstract class MediatedApiBase : IMediatedApi
    {
        protected readonly IBus Bus;
        protected readonly IBusSettings BusSettings;
        protected readonly IMediator Mediator;
        protected readonly ISerializer Serializer;

        protected MediatedApiBase(IMediator mediator, IBus bus, IBusSettings busSettings, ISerializer serializer)
        {
            Mediator = mediator;
            Bus = bus;
            BusSettings = busSettings;
            Serializer = serializer;
        }

        /// <inheritdoc />
        public void SendToBus<TBusCommand>(TBusCommand message, string endpointName = null)
            where TBusCommand : IBusCommand
        {
            endpointName = endpointName ?? BusSettings.GetSendEndpoint(message);
            Bus.Send(endpointName, message);
        }

        /// <inheritdoc />
        public void PublishToBus<TBusMessage>(TBusMessage message) where TBusMessage : IBusMessage
        {
            Bus.Publish(message);
        }

        /// <inheritdoc />
        public void PublishToFrontEnd<TMessage>(TMessage message) where TMessage : IMessageBase
        {
            Mediator.Publish(message);
        }
    }
}