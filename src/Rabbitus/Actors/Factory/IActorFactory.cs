using System;

namespace Rabbitus.Actors.Factory
{
    public interface IActorFactory
    {
        void CreateActor<TActor>(Action<TActor> action) where TActor : Actor<TActor>;
    }
}