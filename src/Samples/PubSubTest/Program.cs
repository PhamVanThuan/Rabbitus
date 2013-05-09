using System;
using System.Linq;
using Rabbitus;

namespace PubSubTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitus = RabbitusFactory.Configure(c => c.Subscribe<SampleActor>());

            Console.WriteLine("Will Start Publishing After Messages Are Published!!!");
            Console.WriteLine("Enter {Message}...");

            var line = Console.ReadLine();

            Enumerable.Range(0, 100000)
                .AsParallel()
                .ForAll(i => rabbitus.Publish(new SampleMessage { Message = line + " : " + i.ToString(), Timestamp = DateTime.Now }));

            rabbitus.Start();
            Console.ReadKey(false);
        }
    }
}
