using System;

namespace Rabbitus.Actors.Factory
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