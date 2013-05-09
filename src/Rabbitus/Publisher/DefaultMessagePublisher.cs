using System.Text;
using Newtonsoft.Json;
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

                channel.BasicPublish("FOO", "", null, body);
            }
        }
    }
}