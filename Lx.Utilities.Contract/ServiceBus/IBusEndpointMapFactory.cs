using System;
using System.Collections.Generic;

namespace Lx.Utilities.Contract.ServiceBus {
    public interface IBusEndpointMapFactory {
        IDictionary<Type, string> GetMaps();
    }
}