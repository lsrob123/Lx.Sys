using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Mediator
{
    /// <summary>
    ///     Only for reflection
    /// </summary>
    public interface IMediatorMessageHandler
    {
    }

    public interface IMediatorMessageHandler<in T> : IMediatorMessageHandler
        where T : IMessageBase
    {
        void Handle(T message);
    }
}