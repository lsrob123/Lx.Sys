namespace Lx.Utilities.Contract.Infrastructure.EventDispacthing {
    public interface IEventDispatchingProxy : IEventDispatcherBase {
        void RegisterDispatcher<TDispatcher>(TDispatcher dispatcher) where TDispatcher : IEventDispatcher;
    }
}