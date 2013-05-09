using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RabbitMQ.Client;
using Rabbitus.Actors;
using Rabbitus.Actors.Configuration;
using Rabbitus.Consumer;
using Rabbitus.InboundDispatcher;
using Rabbitus.Publisher;
using Rabbitus.RabbitMQ;
using Rabbitus.Serialization;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        private readonly IInboundMessageDispatcher _inboundDispatcher;
        private readonly IMessageConsumer _messageConsumer;
        private readonly IMessagePublisher _messagePublisher;

        internal Rabbitus()
        {
            var connection = new RabbitMqConnection();
            var serializer = new JsonMessageSerializer();

            _inboundDispatcher = new InboundMessageDispatcher();
            _messageConsumer = new ThreadedMessageConsumer(_inboundDispatcher, connection, serializer);
            _messagePublisher = new MessagePublisher(connection, serializer);
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
            _messagePublisher.Publish(message);
        }
    }
}