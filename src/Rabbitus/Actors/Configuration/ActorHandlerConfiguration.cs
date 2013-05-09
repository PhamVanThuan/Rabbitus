using System;
using Rabbitus.Context;

namespace Rabbitus.Actors.Configuration
{
    public class ActorHandlerConfiguration<TActor, TMessage> : IActorHandlerConfiguration,
        IConfigureHandledBy<TActor, TMessage>,
        IConfigureHandleWhen<TMessage> 
        where TActor : Actor<TActor>
        where TMessage : class
    {
        protected Action<TActor, IContext<TMessage>> Handler { get; private set; }
        protected Predicate<IContext<TMessage>> CanHandle { get; private set; }

        public ActorHandlerConfiguration()
        {
            CanHandle = context => true;
        }

        public IConfigureHandleWhen<TMessage> HandledBy(Action<TActor, IContext<TMessage>> handler)
        {
            Handler = handler;

            return this;
        }

        public void HandleWhen(Predicate<IContext<TMessage>> predicate)
        {
            CanHandle = predicate;
        }
    }
}