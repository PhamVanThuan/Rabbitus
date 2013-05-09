using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Rabbitus.Actors;
using Rabbitus.Actors.Configuration;
using Rabbitus.Factories;
using Rabbitus.InboundDispatcher.Dispatchers;

namespace Rabbitus.InboundDispatcher.Builder
{
    public class InboundMessageDispatcherBuilder
    {
        private readonly IActorFactory _actorFactory;
        private readonly IList<Type> _actorTypes;

        public InboundMessageDispatcherBuilder(IActorFactory actorFactory, IList<Type> actorTypes)
        {
            _actorFactory = actorFactory;
            _actorTypes = actorTypes;
        }

        public IInboundMessageDispatcher Build()
        {
            var inboundDispatcher = new InboundMessageDispatcher();

            RegisterDispatchersForActors(inboundDispatcher);

            return inboundDispatcher;
        }

        private void RegisterDispatchersForActors(InboundMessageDispatcher inboundDispatcher)
        {
            foreach (var actorType in _actorTypes)
            {
                RuntimeHelpers.RunClassConstructor(actorType.TypeHandle);

                var messageConfigurations =
                    (IEnumerable<IActorMessageConfiguration>)
                        actorType.GetMethod("GetMessageConfigurations", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            .Invoke(null, null);

                foreach (var messageConfiguration in messageConfigurations)
                {
                    var dispatcher = messageConfiguration.CreateDispatcher(_actorFactory);
                    inboundDispatcher.RegisterDispatcher(dispatcher);
                }
            }
        }
    }
}