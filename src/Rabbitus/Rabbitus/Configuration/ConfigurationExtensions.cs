namespace Rabbitus.Configuration
{
    public static class ConfigurationExtensions
    {
         public static IBus WithQueueNamed(this IBus bus, string queue)
         {
             bus.Configuration.QueueName = queue;

             return bus;
         }
    }
}