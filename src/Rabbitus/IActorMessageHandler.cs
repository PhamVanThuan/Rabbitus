namespace Rabbitus
{
    public interface IActorMessageHandler<in TMessage>
        where TMessage : class
    {
        bool CanHandle(IMessageContext<TMessage> context);
        void Handle(IActorFactory actorFactory, IMessageContext<TMessage> context);
    }
}