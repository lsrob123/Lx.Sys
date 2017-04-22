using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Interfaces;

namespace Lx.Utilities.Contract.Mediator
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