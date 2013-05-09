using System;

namespace Rabbitus.Context
{
    public interface IMessageContext<out TMessage>
        where TMessage : class
    {
        string MessageId { get; }
        TMessage Message { get; }
    }
}