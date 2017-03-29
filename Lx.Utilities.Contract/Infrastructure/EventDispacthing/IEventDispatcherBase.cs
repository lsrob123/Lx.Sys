using Lx.Utilities.Contract.Infrastructure.DTOs;

namespace Lx.Utilities.Contract.Infrastructure.EventDispacthing {
    public interface IEventDispatcherBase {
        void Dispatch<TEvent>(TEvent e) where TEvent : ResponseBase;
    }
}