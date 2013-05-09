using System.Text;
using Newtonsoft.Json;
using Rabbitus.RabbitMQ;

namespace Rabbitus.Publisher
{
    public class DefaultMessagePublisher: IMessagePublisher
    {
        private readonly IRabbitMqConnection _connection;

        public DefaultMessagePublisher(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            using (var channel = _connection.CreateModel())
            {
                var data = JsonConvert.SerializeObject(message, new JsonSerializerSettings{TypeNameHandling = TypeNameHandling.All});
                var body = Encoding.UTF8.GetBytes(data);

                channel.BasicPublish("FOO", "", null, body);
            }
        }
    }
}