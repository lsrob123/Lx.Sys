using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Contract.Infrastructure.RequestDispatching;
using Lx.Utilities.Contract.ServiceBus;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb {
    public class NsbRequestDispatcher : IRequestDispatcher {
        protected readonly IBus Bus;
        protected readonly IBusSettings BusSettings;
        protected readonly IDictionary<Type, string> ExceptionalBusEndpointMaps;

        public NsbRequestDispatcher(IBus bus, IBusSettings busSettings,
            IBusEndpointMapFactory exceptionalBusEndpointMapFactory = null) {
            Bus = bus;
            BusSettings = busSettings;

            exceptionalBusEndpointMapFactory = exceptionalBusEndpointMapFactory ?? new DefaultBusEndpointMapFactory();
            ExceptionalBusEndpointMaps = exceptionalBusEndpointMapFactory.GetMaps();
        }

        public void Dispatch<TRequest>(TRequest request) where TRequest : IRequest {
            string busEndpoint;
            if ((ExceptionalBusEndpointMaps == null) ||
                !ExceptionalBusEndpointMaps.TryGetValue(request.GetType(), out busEndpoint))
                busEndpoint = BusSettings.EndpointName;

            Bus.Send(busEndpoint, request);
        }
    }
}