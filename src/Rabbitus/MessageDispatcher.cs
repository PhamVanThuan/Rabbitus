using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Rabbitus.Extensions;

namespace Rabbitus
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IList<IActorMessageHandler<object>> _handlers = new List<IActorMessageHandler<object>>();
        private readonly IActorFactory _actorFactory = new DefaultActorFactory();

        public void RegisterActor<TActor>()
            where TActor : Actor<TActor>
        {
            RuntimeHelpers.RunClassConstructor(typeof(TActor).TypeHandle);
            
            ActorConfiguration<TActor>.RegisteredHandlers()
                .ForEach(handler => _handlers.Add(handler));
        }

        public void Dispatch<TMessage>(IMessageContext<TMessage> context)
            where TMessage : class
        {
            _handlers.Where(handler => handler.CanHandle(context))
                .ForEach(handler => handler.Handle(_actorFactory, context));
        }
    }
}