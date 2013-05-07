namespace Rabbitus
{
    public interface IMessageDispatcher
    {
        void Dispatch<TMessage>(IMessageContext<TMessage> context) 
            where TMessage : class;
    }
}