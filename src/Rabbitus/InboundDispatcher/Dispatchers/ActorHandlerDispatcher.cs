using System;
using Rabbitus.Actors;
using Rabbitus.Context;
using Rabbitus.Factories;

namespace Rabbitus.InboundDispatcher.Dispatchers
{
    public class ActorHandlerDispatcher<TActor, TMessage> : IDispatcher<TMessage>
        where TActor : Actor<TActor>
        where TMessage : class
    {
        private readonly IActorFactory _actorFactory;
        private readonly Action<TActor, IContext<TMessage>> _handler;
        private readonly Predicate<IContext<TMessage>> _canHandle;

        public ActorHandlerDispatcher(IActorFactory actorFactory, Action<TActor, IContext<TMessage>> handler, Predicate<IContext<TMessage>> canHandle)
        {
            _actorFactory = actorFactory;
            _handler = handler;
            _canHandle = canHandle;
        }

        public void Dispatch(IContext<TMessage> context)
        {
            if (!_canHandle(context))
                return;

            _actorFactory.CreateActor<TActor>(actor => _handler(actor, context));
        }
    }
}