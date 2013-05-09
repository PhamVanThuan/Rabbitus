using System;
using System.Text;
using RabbitMQ.Client;
using Rabbitus.RabbitMQ;
using Rabbitus.Serialization;

namespace Rabbitus.Publisher
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMessageSerializer _serializer;
       
        public MessagePublisher(IRabbitMQConnection connection, IMessageSerializer serializer)
        {
            _connection = connection;
            _serializer = serializer;

            _connection.WithSharedChannel(channel =>
            {
                channel.ExchangeDeclare("FOO", ExchangeType.Fanout, true);
                channel.QueueDeclare("FOO", true, false, false, null);
                channel.QueueBind("FOO", "FOO", "", null);
            });
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            _connection.WithSharedChannel(channel =>
            {
                var data = _serializer.SerializeMessage(message);
                var body = Encoding.UTF8.GetBytes(data);
                var properties = channel.CreateBasicProperties();

                properties.MessageId = Guid.NewGuid().ToString();
                properties.ContentType = _serializer.ContentType;

                properties.SetPersistent(true);
                channel.BasicPublish("FOO", "", properties, body);
            });
        }
    }
}