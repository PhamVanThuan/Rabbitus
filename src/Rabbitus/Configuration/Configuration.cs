namespace Rabbitus.Configuration
{
    public class Configuration
    {
        public string RabbitHost { get; set; }
        public int RabbitPort { get; set; }
        public string QueueName { get; set; }

        public Configuration()
        {
            RabbitHost = "localhost";
            RabbitPort = 1572;
        }
    }
}