using System;
using System.Collections.Generic;

namespace Lx.Utilities.Contracts.ServiceBus
{
    public class DefaultBusEndpointMapFactory : IBusEndpointMapFactory
    {
        public IDictionary<Type, string> GetMaps()
        {
            return new Dictionary<Type, string>();
        }
    }
}