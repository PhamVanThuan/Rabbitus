using Rabbitus.Configuration;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        public RabbitusConfiguration Configuration { get; set; }
        public IMessageDispatcher Dispatcher { get; set; }

        public Rabbitus(RabbitusConfiguration configuration, IMessageDispatcher messageDispatcher)
        {
            Configuration = configuration;
            Dispatcher = messageDispatcher;
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
            Dispatcher.Dispatch(new MessageContext<TMessage>(message));
        }

        public void Subscribe<TActor>()
            where TActor : Actor<TActor>
        {
            Dispatcher.RegisterActor<TActor>();
        }
    }
}