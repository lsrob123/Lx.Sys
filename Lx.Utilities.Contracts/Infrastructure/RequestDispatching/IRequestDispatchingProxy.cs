namespace Lx.Utilities.Contracts.Infrastructure.RequestDispatching
{
    public interface IRequestDispatchingProxy : IRequestDispatcherBase
    {
        void RegisterDispatcher<TDispatcher>(TDispatcher dispatcher) where TDispatcher : IRequestDispatcher;
    }
}