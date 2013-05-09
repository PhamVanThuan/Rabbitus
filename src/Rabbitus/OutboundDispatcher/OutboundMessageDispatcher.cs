using System.Text;
using RabbitMQ.Client;
using Rabbitus.Context;
using Rabbitus.RabbitMQ;
using Rabbitus.Serialization;

namespace Rabbitus.OutboundDispatcher
{
    public class OutboundMessageDispatcher : IOutboundMessageDispatcher
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMessageSerializer _serializer;
       
        public OutboundMessageDispatcher(IRabbitMQConnection connection, IMessageSerializer serializer)
        {
            _connection = connection;
            _serializer = serializer;

            _connection.WithChannel(channel =>
            {
                channel.ExchangeDeclare("FOO", ExchangeType.Fanout, true);
                channel.QueueDeclare("FOO", true, false, false, null);
                channel.QueueBind("FOO", "FOO", "", null);
            });
        }

        public void Dispatch<TMessage>(IMessageContext<TMessage> context) where TMessage : class
        {
            _connection.WithChannel(channel =>
            {
                var data = _serializer.SerializeMessage(context.Message);
                var body = Encoding.UTF8.GetBytes(data);
                var properties = channel.CreateBasicProperties();

                properties.MessageId = context.MessageId;
                properties.ContentType = _serializer.ContentType;

                properties.SetPersistent(true);
                channel.BasicPublish("FOO", "", properties, body);
            });
        }
    }
}