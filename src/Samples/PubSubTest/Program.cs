using System;
using System.Linq;
using System.Security.Cryptography;
using Rabbitus;

namespace PubSubTest
{
    class Program
    {
        const int PublishCount = 100000;

        static void Main(string[] args)
        {
            var rabbitus = RabbitusFactory.Configure(c => c.Subscribe<SampleActor>());

            Console.WriteLine("Will Start Publishing After Messages Are Published!!!");
            Console.WriteLine("Hit enter to publish {0} messages...", PublishCount);

            Console.ReadLine();

            Enumerable.Range(1, PublishCount)
                .AsParallel()
                .ForAll(i => rabbitus.Publish(GetSampleMessage(i)));

            rabbitus.Start();
            Console.ReadKey();
        }

        private static SampleMessage GetSampleMessage(int i)
        {
            return new SampleMessage
            {
                Name = "Person" + " " + i.ToString(),
                Age = i,
                Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sed pharetra ipsum. Donec nec gravida odio. Curabitur vulputate orci eget ligula pretium in congue ipsum adipiscing. Pellentesque dictum diam vel justo porttitor vel mattis dui luctus. Ut pretium pulvinar urna, id dapibus dolor mollis malesuada. Donec non massa massa. Nulla facilisi. Nam auctor imperdiet dapibus. Maecenas feugiat sollicitudin nunc, vitae posuere felis consequat vel. Duis feugiat ullamcorper dignissim. Etiam a nisl urna, mollis malesuada elit. Aenean enim velit, tincidunt vel vehicula sit amet, lobortis sed ligula. Nulla facilisi. Suspendisse vitae congue velit.

Integer id odio a nisl venenatis faucibus quis vitae sem. Vivamus aliquet bibendum tristique. Curabitur sollicitudin dui vitae ipsum congue in tincidunt urna consequat. Suspendisse est dolor, gravida quis congue ac, fringilla ac arcu. Etiam lorem elit, semper adipiscing mollis vitae, bibendum sed lacus. Vivamus pellentesque suscipit ullamcorper. Phasellus eget tellus in justo rutrum hendrerit nec id neque. Curabitur ut sem nunc, vitae hendrerit magna. Sed pretium pharetra venenatis. Nunc erat urna, tincidunt sit amet posuere sit amet, laoreet in magna. Maecenas dignissim varius dignissim.

Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque scelerisque risus in justo congue eu ultrices mauris ullamcorper. In leo urna, aliquam nec ultricies a, tempus in dolor. Sed sit amet quam odio, eu fringilla erat. Etiam quis justo in augue imperdiet vulputate. Aenean scelerisque dignissim lorem id consectetur. Vivamus viverra tincidunt vulputate.

Nam urna felis, facilisis id mollis ac, ullamcorper id lorem. Donec felis ante, posuere in congue a, viverra at turpis. Praesent rhoncus vestibulum dui. Duis sed volutpat est. Praesent sed justo nec metus vestibulum laoreet. Curabitur viverra odio massa, at suscipit ante. Donec hendrerit ipsum et lorem pharetra laoreet. Curabitur a erat nibh, id placerat felis. Mauris venenatis malesuada lacus, nec tempus velit pulvinar quis. Maecenas luctus lacinia enim, a cursus mi feugiat sit amet. Quisque vel ligula sem. Praesent ante elit, sollicitudin sit amet tincidunt vitae, fringilla sit amet nulla. Integer egestas volutpat vehicula. Proin non sem mauris.

Donec malesuada cursus odio, in suscipit magna mattis non. Ut auctor ornare iaculis. Cras magna ipsum, hendrerit eget faucibus eget, egestas nec dui. Maecenas at lorem purus. Nunc eu massa et lectus consectetur dictum nec vel lectus. Nam sit amet nibh venenatis est vestibulum facilisis vitae a orci. Proin tempor magna porttitor elit ultrices at accumsan magna consectetur. Mauris nec varius nisl."
            };
        }
    }
}
