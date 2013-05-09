namespace Rabbitus.Context
{
    public class MessageContext<TMessage> : IMessageContext<TMessage>
        where TMessage : class
    {
        public TMessage Message { get; private set; }

        public MessageContext(TMessage message)
        {
            Message = message;
        } 
    }
}