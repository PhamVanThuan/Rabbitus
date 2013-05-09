namespace Rabbitus.Context
{
    public class MessageContext<TMessage> : IMessageContext<TMessage>
        where TMessage : class
    {
        public string MessageId { get; private set; }
        public TMessage Message { get; private set; }

        public MessageContext(string messageId, TMessage message)
        {
            MessageId = messageId;
            Message = message;
        } 
    }
}