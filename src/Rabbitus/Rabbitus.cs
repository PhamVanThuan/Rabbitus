using Rabbitus.Actors;
using Rabbitus.Configuration;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        public RabbitusConfiguration Configuration { get; set; }

        public Rabbitus(RabbitusConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
        }

        public void Subscribe<TActor>()
            where TActor : Actor<TActor>
        {
        }
    }
}