using System.Collections.Generic;

namespace Rabbitus.Actors.Configuration
{
    public static class ActorConfiguration<TActor> 
        where TActor : Actor<TActor>
    {
//// ReSharper disable StaticFieldInGenericType
        private static readonly IList<IActorHandlerConfiguration> Handlers = new List<IActorHandlerConfiguration>();
//// ReSharper restore StaticFieldInGenericType
  
        public static IConfigureHandledBy<TActor, TMessage> ForMessage<TMessage>() 
            where TMessage : class
        {
            var handler = new ActorHandlerConfiguration<TActor, TMessage>();
            Handlers.Add(handler);

            return handler;
        }
    }
}