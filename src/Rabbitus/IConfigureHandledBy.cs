using System;

namespace Rabbitus
{
    public interface IConfigureHandledBy<out TActor, out TMessage>
        where TActor : Actor<TActor>
        where TMessage : class
    {
        IConfigureHandleWhen<TMessage> HandledBy(Action<TActor, IMessageContext<TMessage>> handler);
    }
}