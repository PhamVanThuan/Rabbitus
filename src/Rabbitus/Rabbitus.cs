using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Rabbitus.Actors;
using Rabbitus.Actors.Configuration;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher;
using Rabbitus.MessageConsumer;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        private readonly InboundMessageDispatcher _inboundDispatcher;
        private readonly DefaultMessageConsumer _messageConsumer;

        internal Rabbitus()
        {
            _inboundDispatcher = new InboundMessageDispatcher();
            _messageConsumer = new DefaultMessageConsumer(_inboundDispatcher);
        }

        public void Start()
        {
            _messageConsumer.Start();
        }

        public void Subscribe<TActor>()
            where TActor : Actor<TActor>
        {
            RuntimeHelpers.RunClassConstructor(typeof(TActor).TypeHandle);

            var messageConfigurations = (IEnumerable<IActorMessageConfiguration>)Actor<TActor>.GetMessageConfigurations();
            foreach (var messageConfiguration in messageConfigurations)
            {
                var dispatcher = messageConfiguration.CreateDispatcher();
                _inboundDispatcher.RegisterDispatcher(dispatcher);
            }
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
            _inboundDispatcher.Dispatch(new MessageContext<TMessage>(message));
        }
    }
}