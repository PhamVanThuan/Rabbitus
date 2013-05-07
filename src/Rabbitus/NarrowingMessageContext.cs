namespace Rabbitus
{
    public class NarrowingMessageContext<TMessage> : IMessageContext<TMessage> 
        where TMessage : class
    {
        private readonly IMessageContext<object> _context;

        public TMessage Message
        {
            get { return (TMessage)_context.Message; }
        }

        public NarrowingMessageContext(IMessageContext<object> context)
        {
            _context = context;
        }
    }
}