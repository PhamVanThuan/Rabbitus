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

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }
    }
}