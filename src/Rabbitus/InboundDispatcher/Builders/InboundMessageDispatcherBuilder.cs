using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Rabbitus.Actors.Configuration;
using Rabbitus.Factories;

namespace Rabbitus.InboundDispatcher.Builders
{
    public class InboundMessageDispatcherBuilder : IBuilder<IInboundMessageDispatcher>
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

            RegisterMessageDispatchersForActors(inboundDispatcher);

            return inboundDispatcher;
        }

        private void RegisterMessageDispatchersForActors(InboundMessageDispatcher inboundDispatcher)
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