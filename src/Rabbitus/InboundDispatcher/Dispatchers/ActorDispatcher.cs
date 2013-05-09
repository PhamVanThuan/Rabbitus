using System;
using Rabbitus.Actors;
using Rabbitus.Actors.Factory;
using Rabbitus.Context;

namespace Rabbitus.InboundDispatcher.Dispatchers
{
    public class ActorDispatcher<TActor, TMessage> : IDispatcher<TMessage>
        where TActor : Actor<TActor>
        where TMessage : class
    {
        private readonly Action<TActor, IMessageContext<TMessage>> _handler;
        private readonly Predicate<IMessageContext<TMessage>> _canHandle;

        public ActorDispatcher(Action<TActor, IMessageContext<TMessage>> handler, Predicate<IMessageContext<TMessage>> canHandle)
        {
            _handler = handler;
            _canHandle = canHandle;
        }

        public void Dispatch(IMessageContext<TMessage> context)
        {
            if (!_canHandle(context))
                return;

            ActorFactory.Current.CreateActor<TActor>(actor => _handler(actor, context));
        }
    }
}