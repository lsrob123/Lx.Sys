using Lx.Utilities.Contract.Infrastructure.Interfaces;
using NServiceBus.Saga;

namespace Lx.Utilities.Services.ServiceBus.Nsb
{
    public static class RequestResponseExtensions
    {
        public static THasSagaId WithSaga<THasSagaId, TSagaData>(this THasSagaId hasSagaId, Saga<TSagaData> saga)
            where THasSagaId : IHasSagaId
            where TSagaData : IContainSagaData, new()
        {
            return hasSagaId.WithSagaData(saga.Data);
        }

        public static THasSagaId WithSagaData<THasSagaId>(this THasSagaId hasSagaId, IContainSagaData sagaData)
            where THasSagaId : IHasSagaId
        {
            return Contract.ServiceBus.RequestResponseExtensions.WithSagaId(hasSagaId, sagaData.Id);
        }
    }
}