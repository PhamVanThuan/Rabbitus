using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Rabbitus.Context;
using Rabbitus.InboundDispatcher;
using Rabbitus.RabbitMQ;

namespace Rabbitus.Consumer
{
    public class DefaultMessageConsumer : IMessageConsumer
    {
        private readonly IInboundMessageDispatcher _inboundDispatcher;
        private readonly IRabbitMqConnection _connection;

        public DefaultMessageConsumer(IInboundMessageDispatcher inboundDispatcher, IRabbitMqConnection connection)
        {
            _inboundDispatcher = inboundDispatcher;
            _connection = connection;
        }

        public void Start()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare("FOO", ExchangeType.Direct);
                channel.QueueDeclare("FOO", true, false, false, null);
                channel.QueueBind("FOO", "FOO", "", null);   
            }

            Task.Factory.StartNew(() =>
            {
                var channel = _connection.CreateModel();
                var consumer = new QueueingBasicConsumer(channel);

                channel.BasicQos(0, 1000, false);
                channel.BasicConsume("FOO", false, consumer);
                
                while (true)
                {
                    try
                    {
                        var e = (BasicDeliverEventArgs) consumer.Queue.Dequeue();
                        var props = e.BasicProperties;
                        var body = Encoding.UTF8.GetString(e.Body);
                        var message = JsonConvert.DeserializeObject(body,
                            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});

                        GetType().GetMethod("DispatchMessage", BindingFlags.Instance | BindingFlags.NonPublic)
                            .MakeGenericMethod(message.GetType())
                            .Invoke(this, new[] { message });

                        channel.BasicAck(e.DeliveryTag, false);
                    }
                    catch (OperationInterruptedException)
                    {
                        // The consumer was removed, either through
                        // channel or connection closure, or through the
                        // action of IModel.BasicCancel().
                        break;
                    }
                }
            });
        }

        public void Stop()
        {
            // Wait and stop consume thread
        }

        protected void DispatchMessage<TMessage>(TMessage message)
            where TMessage : class
        {
            var context = new MessageContext<TMessage>(message);
            _inboundDispatcher.Dispatch(context);
        }
    }
}