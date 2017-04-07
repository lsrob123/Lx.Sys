namespace Lx.Utilities.Contract.Infrastructure.EventBroadcasting {
    public interface IEventBroadcastingProxy : IEventBroadcaster {
        void Register<TBroadcaster>(TBroadcaster broadcaster) where TBroadcaster : IEventBroadcaster;
    }
}