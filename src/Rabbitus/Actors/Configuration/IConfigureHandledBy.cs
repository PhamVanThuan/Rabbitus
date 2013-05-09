using System;
using Rabbitus.Context;

namespace Rabbitus.Actors.Configuration
{
    public interface IConfigureHandledBy<out TActor, out TMessage>
        where TActor : Actor<TActor>
        where TMessage : class
    {
        IConfigureHandleWhen<TMessage> HandledBy(Action<TActor, IContext<TMessage>> handler);
    }
}