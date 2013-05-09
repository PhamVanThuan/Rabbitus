using System;
using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public interface IRabbitMQConnection
    {
        void UseSharedChannel(Action<IModel> action);
        void UsehNewChannel(Action<IModel> action);
    }
}