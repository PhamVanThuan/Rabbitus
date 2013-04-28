using System;
using Rabbitus.Configuration;

namespace Rabbitus
{
    public static class Rabbitus
    {
        public static IBus Configure(Action<BusConfiguration> configure)
        {
            var config = new BusConfiguration();
            configure(config);

            return new Bus(config);
        }
    }
}