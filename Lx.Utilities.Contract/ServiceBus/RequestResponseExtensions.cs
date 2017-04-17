using System;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.ServiceBus
{
    public static class RequestResponseExtensions
    {
        public static THasSagaId WithSagaId<THasSagaId>(this THasSagaId hasSagaId, Guid sid)
            where THasSagaId : IHasSagaId
        {
            hasSagaId.Sid = sid;
            return hasSagaId;
        }

        public static THasSagaId WithSameSagaIdFrom<THasSagaId>(this THasSagaId hasSagaId, IHasSagaId other)
            where THasSagaId : IHasSagaId
        {
            hasSagaId.Sid = other.Sid;
            return hasSagaId;
        }
    }
}