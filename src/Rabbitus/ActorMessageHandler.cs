using System;

namespace Rabbitus
{
    public class ActorMessageHandler<TActor, TMessage> : IActorMessageHandler<TMessage>,
        IConfigureHandledBy<TActor, TMessage>,
        IConfigureHandleWhen<TMessage> 
        where TActor : Actor<TActor>
        where TMessage : class
    {
        protected Action<TActor, IMessageContext<TMessage>> Handler { get; private set; }
        protected Predicate<IMessageContext<TMessage>> CanHandleCheck { get; private set; }

        public ActorMessageHandler()
        {
            CanHandleCheck = context => true;
        }

        public bool CanHandle(IMessageContext<TMessage> context)
        {
            return CanHandleCheck(context);
        }

        public void Handle(IActorFactory actorFactory, IMessageContext<TMessage> context)
        {
            actorFactory.CreateActor<TActor>(actor => Handler(actor, context));
        }
       
        public IConfigureHandleWhen<TMessage> HandledBy(Action<TActor, IMessageContext<TMessage>> handler)
        {
            Handler = handler;

            return this;
        }

        public void HandleWhen(Predicate<IMessageContext<TMessage>> predicate)
        {
            CanHandleCheck = predicate;
        }
    }
}