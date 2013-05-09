using System.Collections.Generic;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.InboundDispatcher
{
    public interface IInboundMessageDispatcher
    {
        void Dispatch<TMessage>(IMessageContext<TMessage> messageContext) where TMessage : class;
        void RegisterDispatcher<TMessage>(IDispatcher<TMessage> dispatcher) where TMessage : class;
        IEnumerable<IDispatcher<object>> GetRegisteredDispatchers();
    }
}