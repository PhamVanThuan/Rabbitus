using System;
using Rabbitus.Configuration;

namespace Rabbitus
{
    public static class RabbitusFactory
    {
        public static IRabbitus Configure(Action<RabbitusConfiguration> configure)
        {
            var configuration = new RabbitusConfiguration();
            var rabbitus = new Rabbitus(configuration);

            configure(configuration);

            return rabbitus;
        }
    }
}