using System.Collections.Generic;
using System.Linq;

namespace Rabbitus
{
    public class MessageDispatcher
    {
        private readonly IList<IActorMessageHandler> _handlers = new List<IActorMessageHandler>(); 

        public void RegisterActor<TActor>()
            where TActor : Actor<TActor>
        {
            var handlers = ActorConfiguration<TActor>.RegisteredHandlers();
            foreach (var handler in handlers)
            {
                _handlers.Add(handler);
            }  
        }

        public void Dispatch(object message)
        {
            var handlers = _handlers.Where(h => h.CanHandle(message));
            foreach (var handler in handlers)
            {
                handler.Handle(message); 
            }
        }
    }
}