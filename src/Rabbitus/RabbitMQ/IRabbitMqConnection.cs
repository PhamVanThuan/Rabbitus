using System;
using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public interface IRabbitMQConnection
    {
        void WithChannel(Action<IModel> action);
    }
}