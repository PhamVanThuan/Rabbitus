using System.Collections.Generic;

namespace Rabbitus
{
    public static class ActorConfiguration<TActor> 
        where TActor : Actor<TActor>
    {
//// ReSharper disable StaticFieldInGenericType
        private static readonly IList<IActorMessageHandler<object>> Handlers = new List<IActorMessageHandler<object>>();
//// ReSharper restore StaticFieldInGenericType
  
        public static ActorMessageHandler<TActor, TMessage> ForMessage<TMessage>() 
            where TMessage : class
        {
            var handler = new ActorMessageHandler<TActor, TMessage>();
            Handlers.Add( new NarrowingActorMessageHandler<TMessage>(handler));

            return handler;
        } 

        public static IList<IActorMessageHandler<object>> RegisteredHandlers()
        {
            return Handlers;
        }
    }
}