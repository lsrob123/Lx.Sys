using System.Threading.Tasks;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;

namespace Lx.Utilities.Contracts.Mediator
{
    public interface IMediator
    {
        IMediator Publish<TMessage>(TMessage message) where TMessage : IMessageBase;
        Task<IMediator> PublishAsync<TMessage>(TMessage message) where TMessage : IMessageBase;
        IMediator Subscribe<TMessage>(IMediatorMessageHandler<TMessage> handler) where TMessage : IMessageBase;
        void RegisterAllHandlers(object o);
    }
}