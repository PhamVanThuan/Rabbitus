﻿using System;
using Rabbitus.Configuration;

namespace Rabbitus
{
    public static class RabbitusFactory
    {
        public static IRabbitus Configure(Action<IRabbitus> configure)
        {
            var configuration = new RabbitusConfiguration();
            var actorFactory = new DefaultActorFactory();
            var messageDispatcher = new MessageDispatcher(actorFactory);
            var rabbitus = new Rabbitus(configuration, messageDispatcher);
            configure(rabbitus);

            return rabbitus;
        }
    }
}