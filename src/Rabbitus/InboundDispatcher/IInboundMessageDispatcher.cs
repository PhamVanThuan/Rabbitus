using Rabbitus.Context;

namespace Rabbitus.InboundDispatcher
{
    public interface IInboundMessageDispatcher
    {
        void Dispatch<TMessage>(IContext<TMessage> context) where TMessage : class;
    }
}