using System;
using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public class RabbitMqConnection : IRabbitMQConnection
    {
        private readonly IConnection _connection;

        public RabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory();

            _connection = connectionFactory.CreateConnection();
        }

        public void WithChannel(Action<IModel> action)
        {
            var model = _connection.CreateModel();

            action(model);
            model.Close(200, "Goodbye");
        }
    }
}