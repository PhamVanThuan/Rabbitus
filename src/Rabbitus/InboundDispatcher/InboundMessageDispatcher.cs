using System.Collections.Generic;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.InboundDispatcher
{
    public class InboundMessageDispatcher : IInboundMessageDispatcher
    {
        private readonly IList<IDispatcher<object>> _dispatchers = new List<IDispatcher<object>>();

        public void Dispatch<TMessage>(IContext<TMessage> context) where TMessage : class
        {
            foreach (var dispatcher in _dispatchers)
                dispatcher.Dispatch(context);
        }
    }
}