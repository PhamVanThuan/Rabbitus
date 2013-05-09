using Rabbitus.Context;
using Rabbitus.InboundDispatcher;

namespace Rabbitus.Publisher
{
    public class DefaultMessagePublisher: IMessagePublisher
    {
        private readonly IInboundMessageDispatcher _inboundDispatcher;

        public DefaultMessagePublisher(IInboundMessageDispatcher inboundDispatcher)
        {
            _inboundDispatcher = inboundDispatcher;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            _inboundDispatcher.Dispatch(new MessageContext<TMessage>(message));
        }
    }
}