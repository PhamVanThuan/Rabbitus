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
            Console.WriteLine("Enter {Text}...");

            var line = Console.ReadLine();

            Enumerable.Range(1, 100000)
                .AsParallel()
                .ForAll(i => rabbitus.Publish(new SampleMessage { Text = line + " " + i.ToString() }));

            rabbitus.Start();
            Console.ReadKey();
        }
    }
}
