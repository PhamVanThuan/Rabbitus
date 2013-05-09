using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public interface IRabbitMQConnection
    {
        IModel CreateModel();
    }
}