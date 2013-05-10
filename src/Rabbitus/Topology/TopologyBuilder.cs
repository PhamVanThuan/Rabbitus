using System;
using RabbitMQ.Client;
using Rabbitus.InboundDispatcher;
using Rabbitus.RabbitMQ;

namespace Rabbitus.Topology
{
    public class TopologyBuilder : ITopologyBuilder
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IInboundMessageDispatcher _inboundDispatcher;
        private readonly string _applicationQueue;

        public TopologyBuilder(IRabbitMQConnection connection, IInboundMessageDispatcher inboundDispatcher)
        {
            _connection = connection;
            _inboundDispatcher = inboundDispatcher;
            _applicationQueue = "APPLICATION_QUEUE";
        }

        public void BuildTopology()
        {
            CreateApplicationQueue();

            foreach (var dispatcher in _inboundDispatcher.GetRegisteredDispatchers())
            {
                Console.WriteLine(dispatcher.MessageType);
            }
        }

        private void CreateApplicationQueue()
        {
            _connection.WithChannel(channel => channel.QueueDeclare(_applicationQueue, true, false, false, null));
        }

        private void CreateExchangeAndBindToApplicationQueue(string exchangeName)
        {
            _connection.WithChannel(channel =>
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);
                channel.QueueBind(_applicationQueue, exchangeName, "", null);
            });
        }
    }
}