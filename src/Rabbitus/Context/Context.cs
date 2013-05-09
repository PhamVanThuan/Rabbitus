namespace Rabbitus.Context
{
    public class Context<TMessage> : IContext<TMessage>
        where TMessage : class
    {
        public TMessage Message { get; private set; }

        public Context(TMessage message)
        {
            Message = message;
        } 
    }
}