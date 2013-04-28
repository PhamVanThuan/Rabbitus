using Rabbitus.Configuration;

namespace Rabbitus
{
    public class Bus : IBus
    {
        public BusConfiguration Configuration { get; private set; }

        public Bus(BusConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}