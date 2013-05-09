using System.Collections.Concurrent;
using System.Collections.Generic;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.InboundDispatcher
{
    public class InboundMessageDispatcher : IInboundMessageDispatcher
    {
        private readonly ConcurrentBag<IDispatcher<object>> _dispatchers = new ConcurrentBag<IDispatcher<object>>();

        public void Dispatch<TMessage>(IMessageContext<TMessage> messageContext) where TMessage : class
        {
            foreach (var dispatcher in _dispatchers)
                dispatcher.Dispatch(messageContext);
        }

        public void RegisterDispatcher<TMessage>(IDispatcher<TMessage> dispatcher)
            where TMessage : class
        {
            _dispatchers.Add(new NarrowingDispatcher<TMessage>(dispatcher));
        }

        public IEnumerable<IDispatcher<object>> GetRegisteredDispatchers()
        {
            return _dispatchers;
        }
    }
}