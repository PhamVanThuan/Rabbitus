using Rabbitus.Context;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.InboundDispatcher
{
    public class NarrowingDispatcher<TMessage> : IDispatcher<object>
        where TMessage : class
    {
        private readonly IDispatcher<TMessage> _dispatcher;

        public NarrowingDispatcher(IDispatcher<TMessage> dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Dispatch(IContext<object> context)
        {
            var message = context.Message as TMessage;
            if (message == null)
                return;

            _dispatcher.Dispatch(new Context<TMessage>(message));
        }
    }
}