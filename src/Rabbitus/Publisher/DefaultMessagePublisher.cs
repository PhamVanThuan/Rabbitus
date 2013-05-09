using System;
using System.Text;
using RabbitMQ.Client.Framing.v0_8;
using Rabbitus.RabbitMQ;
using Rabbitus.Serialization;

namespace Rabbitus.Publisher
{
    public class DefaultMessagePublisher: IMessagePublisher
    {
        private readonly IRabbitMqConnection _connection;
        private readonly IMessageSerializer _serializer;

        public DefaultMessagePublisher(IRabbitMqConnection connection, IMessageSerializer serializer)
        {
            _connection = connection;
            _serializer = serializer;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            using (var channel = _connection.CreateModel())
            {
                var data = _serializer.SerializeMessage(message);
                var body = Encoding.UTF8.GetBytes(data);
                var properties = channel.CreateBasicProperties();

                properties.MessageId = Guid.NewGuid().ToString();
                properties.ContentType = _serializer.ContentType;

                properties.SetPersistent(true);
                channel.BasicPublish("FOO", "", properties, body);
            }
        }
    }
}