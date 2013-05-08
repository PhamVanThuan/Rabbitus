using System;

namespace Rabbitus
{
    public interface IConfigureHandleWhen<out TMessage>
        where TMessage : class
    {
        void HandleWhen(Predicate<IMessageContext<TMessage>> predicate);
    }
}