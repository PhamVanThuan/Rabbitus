using System.Collections.Generic;
using Rabbitus.Actors.Configuration;

namespace Rabbitus.Actors
{
    public class Actor<TActor>
        where TActor : Actor<TActor>
    {
//// ReSharper disable StaticFieldInGenericType
        private static readonly IList<IActorMessageConfiguration> MessageConfigurations = new List<IActorMessageConfiguration>();
//// ReSharper restore StaticFieldInGenericType

        public static IConfigureHandledBy<TActor, TMessage> ForMessage<TMessage>()
            where TMessage : class
        {
            var handler = new ActorMessageConfiguration<TActor, TMessage>();
            MessageConfigurations.Add(handler);

            return handler;
        }

        public static IList<IActorMessageConfiguration> GetMessageConfigurations()
        {
            return MessageConfigurations;
        }
    }
}