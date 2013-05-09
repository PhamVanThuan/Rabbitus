﻿using Rabbitus.Actors.Configuration;

namespace Rabbitus.Actors
{
    public class Actor<TActor>
        where TActor : Actor<TActor>
    {
        public static IConfigureHandledBy<TActor, TMessage> ForMessage<TMessage>()
            where TMessage : class
        {
            return ActorConfiguration<TActor>.ForMessage<TMessage>();
        }
    }
}