using System;
using System.Collections.Generic;
using System.Linq;
using Lx.Utilities.Services.Infrastructure;
using Microsoft.AspNet.SignalR;

namespace Lx.Utilities.Services.SignalR
{
    public static class HubTypeHelper
    {
        public static readonly Type HubTypeBase = typeof(Hub);

        public static IReadOnlyCollection<Type> GetHubTypes()
        {
            var hubTypes = AssemblyHelper.GetReferencedAssemblies().SelectMany(x => x.GetTypes())
                .Where(t => HubTypeBase.IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            return hubTypes;
        }
    }
}