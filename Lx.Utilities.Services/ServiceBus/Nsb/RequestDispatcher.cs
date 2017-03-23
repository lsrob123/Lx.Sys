using System;
using System.Collections.Generic;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.DTO;
using Lx.Utilities.Contract.ServiceBus;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.NSB {
    public class RequestDispatcher : IRequestDispatcher {
        protected readonly IBus Bus;
        protected readonly IBusSettings BusSettings;
        protected readonly IDictionary<Type, string> ExceptionalBusEndpointMaps;

        public RequestDispatcher(IBus bus, IBusSettings busSettings,
            IBusEndpointMapFactory exceptionalBusEndpointMapFactory) {
            Bus = bus;
            BusSettings = busSettings;

            ExceptionalBusEndpointMaps = exceptionalBusEndpointMapFactory?.GetMaps();
        }

        public void Dispatch(IRequest request) {
            string busEndpoint;
            if ((ExceptionalBusEndpointMaps == null) ||
                !ExceptionalBusEndpointMaps.TryGetValue(request.GetType(), out busEndpoint))
                busEndpoint = BusSettings.EndpointName;

            Bus.Send(busEndpoint, request);
        }
    }
}