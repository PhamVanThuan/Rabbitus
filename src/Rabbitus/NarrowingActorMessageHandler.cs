namespace Rabbitus
{
    public class NarrowingActorMessageHandler<TMessage> : IActorMessageHandler<object> 
        where TMessage : class
    {
        private readonly IActorMessageHandler<TMessage> _handler;

        public NarrowingActorMessageHandler(IActorMessageHandler<TMessage> handler)
        {
            _handler = handler;
        }

        public bool CanHandle(IMessageContext<object> context)
        {
            var message = context.Message as TMessage;

            return message != null && _handler.CanHandle(new MessageContext<TMessage>(message));
        }

        public void Handle(IActorFactory actorFactory, IMessageContext<object> context)
        {
            _handler.Handle(actorFactory, new NarrowingMessageContext<TMessage>(context));
        }
    }
}