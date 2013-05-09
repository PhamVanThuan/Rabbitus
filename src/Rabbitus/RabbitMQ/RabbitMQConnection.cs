﻿using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnection _connection;
        
        public RabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory();
            _connection = connectionFactory.CreateConnection();
        }

        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }
    }
}