using Rabbitus.Configuration;

namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        public RabbitusConfiguration Configuration { get; private set; }

        public Rabbitus()
        {
            Configuration = new RabbitusConfiguration();
        }
    }
}