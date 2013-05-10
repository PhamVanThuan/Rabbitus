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
    public class ThreadedMessageConsumer : IMessageConsumer
    {
        private readonly IInboundMessageDispatcher _inboundMessageDispatcher;
        private readonly IRabbitMQConnection _connection;
        private readonly IMessageSerializer _serializer;

        public ThreadedMessageConsumer(
            IInboundMessageDispatcher inboundMessageDispatcher, 
            IRabbitMQConnection connection,
            IMessageSerializer serializer)
        {
            _inboundMessageDispatcher = inboundMessageDispatcher;
            _connection = connection;
            _serializer = serializer;
        }

        public void Start()
        {
            Enumerable.Range(0, 4)
                .ForEach(i => StartConsumerThread());
        }

        public void Stop()
        {
            // Wait and stop consume thread
        }

        private void StartConsumerThread()
        {
            Task.Factory.StartNew(Consume)
                .ContinueWith(t => StartConsumerThread());
        }
        
        protected void Consume()
        {
            using (var channel = _connection.CreateChannel())
            {
                var consumer = new QueueingBasicConsumer(channel);

                channel.BasicQos(0, 1000, false);
                channel.BasicConsume("FOO", false, consumer);

                try
                {
                    while (true)
                    {
                        var e = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = Encoding.UTF8.GetString(e.Body);
                        var message = _serializer.DeserializeMessage(body);

                        GetType()
                            .GetMethod("DispatchMessage", BindingFlags.Instance | BindingFlags.NonPublic)
                            .MakeGenericMethod(message.GetType())
                            .Invoke(this, new[] { message, e.BasicProperties });

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

        protected void DispatchMessage<TMessage>(TMessage message, IBasicProperties properties)
            where TMessage : class
        {
            var context = new MessageContext<TMessage>(properties.MessageId, message);
            _inboundMessageDispatcher.Dispatch(context);
        }
    }
}