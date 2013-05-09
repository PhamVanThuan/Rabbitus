using System;
using Rabbitus.Consumer;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher;
using Rabbitus.OutboundDispatcher;
using Rabbitus.RabbitMQ;
using Rabbitus.Serialization;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        public IInboundMessageDispatcher InboundDispatcher { get; private set; }
        public IOutboundMessageDispatcher OutboundDispatcher { get; private set; }
        public IMessageConsumer MessageConsumer { get; private set; }

        internal Rabbitus()
        {
            var connection = new RabbitMqConnection();
            var serializer = new JsonMessageSerializer();

            InboundDispatcher = new InboundMessageDispatcher();
            OutboundDispatcher = new OutboundMessageDispatcher(connection, serializer);
            MessageConsumer = new ThreadedMessageConsumer(InboundDispatcher, connection, serializer);
        }

        public void Start()
        {
            MessageConsumer.Start();
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
            var messageId = Guid.NewGuid().ToString();
            var context = new MessageContext<TMessage>(messageId, message);

            OutboundDispatcher.Dispatch(context);
        }
    }
}