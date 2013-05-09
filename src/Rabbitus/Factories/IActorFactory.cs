using System;
using Rabbitus.Actors;

namespace Rabbitus.Factories
{
    public interface IActorFactory
    {
        void CreateActor<TActor>(Action<TActor> action) where TActor : Actor<TActor>;
    }
}