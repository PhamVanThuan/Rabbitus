using System;

namespace Rabbitus
{
    public interface IActorFactory
    {
        void CreateActor<TActor>(Action<TActor> action) where TActor : Actor<TActor>;
    }
}