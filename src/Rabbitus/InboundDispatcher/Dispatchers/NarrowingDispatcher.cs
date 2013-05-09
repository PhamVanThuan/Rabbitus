using Rabbitus.Context;

namespace Rabbitus.InboundDispatcher.Dispatchers
{
    public class NarrowingDispatcher<TMessage> : IDispatcher<object>
        where TMessage : class
    {
        private readonly IDispatcher<TMessage> _dispatcher;

        public NarrowingDispatcher(IDispatcher<TMessage> dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Dispatch(IMessageContext<object> messageContext)
        {
            var message = messageContext.Message as TMessage;
            if (message == null)
                return;

            _dispatcher.Dispatch(new MessageContext<TMessage>(message));
        }
    }
}