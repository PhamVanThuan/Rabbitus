using System.Collections.Generic;
using System.Linq;

namespace Rabbitus
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IList<IActorMessageHandler<object>> _handlers = new List<IActorMessageHandler<object>>();
        private readonly IActorFactory _actorFactory = new DefaultActorFactory();

        public void RegisterActor<TActor>()
            where TActor : Actor<TActor>
        {
            var handlers = ActorConfiguration<TActor>.RegisteredHandlers();
            foreach (var handler in handlers)
            {
                _handlers.Add(handler);
            }  
        }

        public void Dispatch<TMessage>(IMessageContext<TMessage> context)
            where TMessage : class
        {
            var handlers = _handlers.Where(h => h.CanHandle(context));
            foreach (var handler in handlers)
            {
                handler.Handle(_actorFactory, context); 
            }
        }
    }
}