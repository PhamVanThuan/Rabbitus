using System;
using Rabbitus.Actors;
using Rabbitus.Context;
using Rabbitus.Factories;

namespace Rabbitus.InboundDispatcher.Dispatchers
{
    public class ActorMessageDispatcher<TActor, TMessage> : IDispatcher<TMessage>
        where TActor : Actor<TActor>
        where TMessage : class
    {
        private readonly IActorFactory _actorFactory;
        private readonly Action<TActor, IMessageContext<TMessage>> _handler;
        private readonly Predicate<IMessageContext<TMessage>> _canHandle;

        public ActorMessageDispatcher(IActorFactory actorFactory, Action<TActor, IMessageContext<TMessage>> handler, Predicate<IMessageContext<TMessage>> canHandle)
        {
            _actorFactory = actorFactory;
            _handler = handler;
            _canHandle = canHandle;
        }

        public void Dispatch(IMessageContext<TMessage> messageContext)
        {
            if (!_canHandle(messageContext))
                return;

            _actorFactory.CreateActor<TActor>(actor => _handler(actor, messageContext));
        }
    }
}