using System;
using System.Linq;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.NSB
{
    public class MessageHandlerAdapterLister : ISpecifyMessageHandlerOrdering
    {
        private static readonly Type ResponseType = typeof (IResponse),
            NotToBeMediatedAttributeType = typeof (NotToBeMediatedAttribute);

        public void SpecifyOrder(Order order)
        {
            var responseTypes = AssemblyHelper.GetTypesInReferencedAssemblies(
                typeFilter: t =>
                    t.IsInstanceOfType(ResponseType) &&
                    t.CustomAttributes.All(a => a.AttributeType != NotToBeMediatedAttributeType));

            var adapterTypes = responseTypes
                .Select(responseType => typeof (MessageHandlerAdapter<>)
                    .MakeGenericType(responseType)).ToList();

            order.Specify(adapterTypes.ToArray());
        }
    }
}