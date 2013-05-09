using System;
using Rabbitus.Context;

namespace Rabbitus.Actors.Configuration
{
    public interface IConfigureHandleWhen<out TMessage>
        where TMessage : class
    {
        void HandleWhen(Predicate<IMessageContext<TMessage>> predicate);
    }
}