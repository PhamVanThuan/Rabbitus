namespace Rabbitus.Context
{
    public interface IContext<out TMessage>
        where TMessage : class
    {
        TMessage Message { get; }
    }
}