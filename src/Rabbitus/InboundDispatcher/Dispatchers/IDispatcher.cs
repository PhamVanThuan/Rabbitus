using Rabbitus.Context;

namespace Rabbitus.InboundDispatcher.Dispatchers
{
    public interface IDispatcher<in TMessage>
        where TMessage : class
    {
        void Dispatch(IMessageContext<TMessage> context);
    }
}