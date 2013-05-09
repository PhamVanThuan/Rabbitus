using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Rabbitus.Context;
using Rabbitus.Extensions;
using Rabbitus.InboundDispatcher;
using Rabbitus.RabbitMQ;
using Rabbitus.Serialization;

namespace Rabbitus.Consumer
{
    public class DefaultMessageConsumer : IMessageConsumer
    {
        private readonly IInboundMessageDispatcher _inboundDispatcher;
        private readonly IRabbitMqConnection _connection;
        private readonly IMessageSerializer _serializer;

        public DefaultMessageConsumer(
            IInboundMessageDispatcher inboundDispatcher, 
            IRabbitMqConnection connection,
            IMessageSerializer serializer)
        {
            _inboundDispatcher = inboundDispatcher;
            _connection = connection;
            _serializer = serializer;
        }

        public void Start()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare("FOO", ExchangeType.Direct);
                channel.QueueDeclare("FOO", true, false, false, null);
                channel.QueueBind("FOO", "FOO", "", null);   
            }

            Enumerable.Range(0, 5)
                .ForEach(i => StartConsumerThread());
        }

        private void StartConsumerThread()
        {
            Task.Factory.StartNew(Consume)
                .ContinueWith(t => StartConsumerThread());
        }

        public void Stop()
        {
            // Wait and stop consume thread
        }

        protected void Consume()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.BasicQos(0, 1000, false);
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume("FOO", false, consumer);

                try
                {
                    while (true)
                    {
                        var e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var props = e.BasicProperties;
                        var body = Encoding.UTF8.GetString(e.Body);
                        var message = _serializer.DeserializeMessage(body);

                        GetType()
                            .GetMethod("DispatchMessage", BindingFlags.Instance | BindingFlags.NonPublic)
                            .MakeGenericMethod(message.GetType())
                            .Invoke(this, new[] {message});

                        channel.BasicAck(e.DeliveryTag, false);
                    }
                }
                catch (OperationInterruptedException)
                {
                    // The consumer was removed, either through
                    // channel or connection closure, or through the
                    // action of IModel.BasicCancel().
                } 
            }
        }

        protected void DispatchMessage<TMessage>(TMessage message)
            where TMessage : class
        {
            var context = new MessageContext<TMessage>(message);
            _inboundDispatcher.Dispatch(context);
        }
    }
}