using System;

namespace Rabbitus
{
    public class ActorMessageHandler<TActor, TMessage> : IActorMessageHandler
        where TActor : Actor<TActor>
    {
        public Action<TActor, object> Handler { get; private set; }

        public Type MessageType
        {
            get { return typeof (TMessage); }
        }

        public bool CanHandle(object message)
        {
            return message is TMessage;
        }

        public void Handle(object message)
        {
            var actor = Activator.CreateInstance<TActor>();
            Handler(actor, message);
        }

        public void HandledBy(Action<TActor, TMessage> handler)
        {
            Handler = (actor, message) => handler(actor, (TMessage) message);
        }
    }
}