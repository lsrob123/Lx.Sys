using System;
using System.Collections.Generic;

namespace Lx.Utilities.Contracts.ServiceBus
{
    public interface IBusEndpointMapFactory
    {
        IDictionary<Type, string> GetMaps();
    }
}