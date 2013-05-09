using System;
using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public class RabbitMqConnection : IRabbitMQConnection
    {
        private static readonly object ModelLock = new object();

        private readonly IConnection _connection;
        private readonly IModel _model;

        public RabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory();

            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
        }

        public void WithSharedChannel(Action<IModel> action)
        {
            lock (ModelLock)
            {
                action(_model);
            }
        }

        public void WithNewChannel(Action<IModel> action)
        {
            var model = _connection.CreateModel();

            action(model);
            model.Close(200, "Goodbye");
        }
    }
}