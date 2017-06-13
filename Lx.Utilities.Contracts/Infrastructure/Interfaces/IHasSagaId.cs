using System;

namespace Lx.Utilities.Contracts.Infrastructure.Interfaces
{
    public interface IHasSagaId
    {
        /// <summary>
        ///     Saga look up key
        /// </summary>
        /// <remarks>
        ///     The property needs to be serializable as it will be transferred with MQ and service bus.
        ///     Thus the full name cannot be exposed to client side and we use "Sid" instead of "SagaId".
        /// </remarks>
        Guid Sid { get; set; }
    }
}