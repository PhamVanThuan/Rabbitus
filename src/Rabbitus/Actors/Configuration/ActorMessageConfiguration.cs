using System;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.Actors.Configuration
{
    public class ActorMessageConfiguration<TActor, TMessage> : IActorMessageConfiguration,
        IConfigureHandledBy<TActor, TMessage>,
        IConfigureHandleWhen<TMessage> 
        where TActor : Actor<TActor>
        where TMessage : class
    {
        public Action<TActor, IMessageContext<TMessage>> Handler { get; private set; }
        public Predicate<IMessageContext<TMessage>> CanHandle { get; private set; }

        public Type MessageType
        {
            get { return typeof (TMessage); }
        }

        public ActorMessageConfiguration()
        {
            CanHandle = context => true;
        }

        public IConfigureHandleWhen<TMessage> HandledBy(Action<TActor, IMessageContext<TMessage>> handler)
        {
            Handler = handler;

            return this;
        }

        public void HandleWhen(Predicate<IMessageContext<TMessage>> predicate)
        {
            CanHandle = predicate;
        }

        public IDispatcher<object> CreateDispatcher()
        {
            var dispatcher = new ActorDispatcher<TActor, TMessage>(Handler, CanHandle);
            return new NarrowingDispatcher<TMessage>(dispatcher);
        }
    }
}