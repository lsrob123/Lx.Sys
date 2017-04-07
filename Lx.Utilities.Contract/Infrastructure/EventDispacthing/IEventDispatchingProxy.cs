namespace Lx.Utilities.Contract.Infrastructure.EventDispacthing {
    public interface IEventDispatchingProxy : IEventDispatcher {
        void RegisterDispatcher<TDispatcher>(TDispatcher dispatcher) where TDispatcher : IEventDispatcher;
    }
}