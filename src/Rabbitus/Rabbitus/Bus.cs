using System;

namespace Rabbitus
{
	public class Bus : IBus
	{
        public Configuration.Configuration Configuration { get; private set; }

        private Bus()
        {
            Configuration = new Configuration.Configuration();
        }

		public static IBus Create(Action<IBus> configure)
		{
		    var bus = new Bus();
		    configure(bus);

		    return bus;
		}
	}
}