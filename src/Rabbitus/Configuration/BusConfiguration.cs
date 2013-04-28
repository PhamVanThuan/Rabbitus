namespace Rabbitus.Configuration
{
    public class BusConfiguration
    {
        public string RabbitHost { get; set; }
        public int RabbitPort { get; set; }
        public string QueueName { get; set; }

        public BusConfiguration()
        {
            RabbitHost = "localhost";
            RabbitPort = 1572;
        }
    }
}