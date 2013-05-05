using System;

namespace Rabbitus
{
    public interface IActorMessageHandler
    {
        Type MessageType { get; }
        bool CanHandle(object message);
        void Handle(object message);
    }
}