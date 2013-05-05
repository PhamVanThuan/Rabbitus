using System;

namespace Rabbitus
{
    public static class RabbitusFactory
    {
        public static IRabbitus Configure(Action<IRabbitus> configure)
        {
            var rabbitus = new Rabbitus();
            configure(rabbitus);

            return rabbitus;
        }
    }
}