using Rabbitus.Context;

namespace Rabbitus.OutboundDispatcher
{
    public interface IOutboundMessageDispatcher
    {
        void Dispatch<TMessage>(IMessageContext<TMessage> context) where TMessage : class;
    }
}