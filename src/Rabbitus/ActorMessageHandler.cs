using System;

namespace Rabbitus
{
    public class ActorMessageHandler<TActor, TMessage> : IActorMessageHandler<TMessage>
        where TActor : Actor<TActor>
        where TMessage : class
    {
        public Action<TActor, IMessageContext<TMessage>> Handler { get; private set; }

        public bool CanHandle(IMessageContext<TMessage> context)
        {
            return true;
        }

        public void Handle(IActorFactory actorFactory, IMessageContext<TMessage> context)
        {
            actorFactory.CreateActor<TActor>(actor => Handler(actor, context));
        }

        public void HandledBy(Action<TActor, IMessageContext<TMessage>> handler)
        {
            Handler = handler;
        }
    }
}