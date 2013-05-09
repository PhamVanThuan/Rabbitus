using RabbitMQ.Client;

namespace Rabbitus.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        IModel CreateModel();
    }
}