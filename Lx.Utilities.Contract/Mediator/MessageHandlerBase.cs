using Lx.Utilities.Contract.Infrastructure.Common;

namespace Lx.Utilities.Contract.Mediator {
    public abstract class MessageHandlerBase<TMessage> : IMediatorMessageHandler<TMessage>
        where TMessage : class, IMessageBase {
        protected MessageHandlerBase(IMediator mediator = null) {
            if (mediator == null)
                mediator = Mediator.Default;

            mediator.Subscribe(this);
        }

        public abstract void Handle(TMessage message);
    }
}