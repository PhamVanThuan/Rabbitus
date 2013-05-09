using System;
using System.Linq;
using Rabbitus;

namespace PubSubTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitus = RabbitusFactory.Configure(c => { });

            //rabbitus.Subscribe<SampleActor>();
            //rabbitus.Start();

            while (true)
            {
                Console.WriteLine("Enter {Message}...");

                var line = Console.ReadLine();
                
                Enumerable.Range(0, 1000000)
                    .AsParallel()
                    .ForAll(i => rabbitus.Publish(new SampleMessage { Message = line + " : " + i.ToString(), Timestamp = DateTime.Now }));
            }
        }
    }
}
