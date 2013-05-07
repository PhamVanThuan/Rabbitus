using Rabbitus.Configuration;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        public RabbitusConfiguration Configuration { get; private set; }
        public IMessageDispatcher Dispatcher { get; private set; }

        public Rabbitus()
        {
            Configuration = new RabbitusConfiguration();
            Dispatcher = new MessageDispatcher();
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
            Dispatcher.Dispatch(new MessageContext<TMessage>(message));
        }
    }
}