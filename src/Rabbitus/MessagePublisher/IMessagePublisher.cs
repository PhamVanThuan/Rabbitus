namespace Rabbitus.MessagePublisher
{
    public interface IMessagePublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : class;
    }
}