using Rabbitus.Factories;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.Actors.Configuration
{
    public interface IActorMessageConfiguration
    {
        IDispatcher<object> CreateDispatcher(IActorFactory actorFactory);
    }
}