    using System.Collections.Generic;

namespace Rabbitus
{
    public static class ActorConfiguration<TActor> 
        where TActor : Actor<TActor>
    {
//// ReSharper disable StaticFieldInGenericType
        private static readonly IList<IActorMessageHandler> Handlers = new List<IActorMessageHandler>();
//// ReSharper restore StaticFieldInGenericType
  
        public static ActorMessageHandler<TActor, TMessage> ForMessage<TMessage>()
        {
            var config = new ActorMessageHandler<TActor, TMessage>();
            Handlers.Add(config);

            return config;
        } 

        public static IList<IActorMessageHandler> RegisteredHandlers()
        {
            return Handlers;
        }
    }
}