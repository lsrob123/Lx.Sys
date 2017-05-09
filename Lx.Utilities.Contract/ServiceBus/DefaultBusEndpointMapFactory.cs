using System;
using System.Collections.Generic;

namespace Lx.Utilities.Contract.ServiceBus {
    public class DefaultBusEndpointMapFactory : IBusEndpointMapFactory {
        public IDictionary<Type, string> GetMaps() {
            return new Dictionary<Type, string>();
        }
    }
}