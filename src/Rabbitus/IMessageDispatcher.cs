namespace Rabbitus
{
    public interface IMessageDispatcher
    {
        void RegisterActor<TActor>() where TActor : Actor<TActor>;
        void Dispatch<TMessage>(IMessageContext<TMessage> context) where TMessage : class;
    }
}