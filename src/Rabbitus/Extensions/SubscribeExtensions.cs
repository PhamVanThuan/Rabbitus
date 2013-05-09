using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Rabbitus.Actors;
using Rabbitus.Actors.Configuration;

namespace Rabbitus.Extensions
{
    public static class SubscribeExtensions
    {
        public static void Subscribe<TActor>(this IRabbitus rabbitus)
            where TActor : Actor<TActor>
        {
            RuntimeHelpers.RunClassConstructor(typeof(TActor).TypeHandle);

            var messageConfigurations = (IEnumerable<IActorMessageConfiguration>)Actor<TActor>.GetMessageConfigurations();
            foreach (var messageConfiguration in messageConfigurations)
            {
                var dispatcher = messageConfiguration.CreateDispatcher();
                rabbitus.InboundDispatcher.RegisterDispatcher(dispatcher);
            }
        }
    }
}