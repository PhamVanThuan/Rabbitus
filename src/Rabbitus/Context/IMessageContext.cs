namespace Rabbitus.Context
{
    public interface IMessageContext<out TMessage>
        where TMessage : class
    {
        TMessage Message { get; }
    }
}