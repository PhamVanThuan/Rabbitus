namespace Rabbitus.Publisher
{
    public interface IOutboundMessageDispatcher
    {
        void Publish<TMessage>(TMessage message) where TMessage : class;
    }
}