namespace Rabbitus
{
    public interface IMessageContext<out TMessage>
        where TMessage : class
    {
        TMessage Message { get; }
    }
}