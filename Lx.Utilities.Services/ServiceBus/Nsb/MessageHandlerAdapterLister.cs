using System;
using System.Linq;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Interfaces;
using Lx.Utilities.Services.Infrastructure;
using NServiceBus;

namespace Lx.Utilities.Services.ServiceBus.Nsb
{
    /// <summary>
    ///     http://stackoverflow.com/questions/9027662/nservicebus-custom-message-handler-type
    /// </summary>
    public class MessageHandlerAdapterLister : ISpecifyMessageHandlerOrdering
    {
        private static readonly Type ResponseType = typeof (IResponse),
            NotToBeMediatedAttributeType = typeof (NotToBeMediatedAttribute);

        public void SpecifyOrder(Order order)
        {
            var adapterTypes = AssemblyHelper.GetTypesInReferencedAssemblies(
                typeFilter: t =>
                    t.IsInstanceOfType(ResponseType) &&
                    t.CustomAttributes.All(a => a.AttributeType != NotToBeMediatedAttributeType))
                .Select(responseType =>
                    typeof (MessageHandlerAdapter<>).MakeGenericType(responseType))
                .ToArray();

            order.Specify(adapterTypes);
        }
    }
}