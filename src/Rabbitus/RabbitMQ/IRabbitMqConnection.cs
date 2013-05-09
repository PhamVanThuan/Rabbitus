using System;
using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public interface IRabbitMQConnection
    {
        void WithSharedChannel(Action<IModel> action);
        void WithNewChannel(Action<IModel> action);
    }
}