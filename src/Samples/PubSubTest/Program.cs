using System;
using System.Linq;
using Rabbitus;
using Rabbitus.Extensions;

namespace PubSubTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitus = RabbitusFactory.Configure(c => { });

            rabbitus.Subscribe<SampleActor>();
            rabbitus.Start();

            while (true)
            {
                Console.WriteLine("Enter {Message}...");

                var line = Console.ReadLine();
                var message = new SampleMessage
                {
                    Message = line,
                    Timestamp = DateTime.Now
                };

                Enumerable.Range(0, 100000)
                    .ForEach(i => rabbitus.Publish(message));
            }
        }
    }
}
