using System;
using Rabbitus.Actors;

namespace Rabbitus.Factories
{
    public class DefaultActorFactory : IActorFactory
    {
        public void CreateActor<TActor>(Action<TActor> action) 
            where TActor : Actor<TActor>
        {
            var actor = Activator.CreateInstance<TActor>();
            action(actor);
        }
    }
}