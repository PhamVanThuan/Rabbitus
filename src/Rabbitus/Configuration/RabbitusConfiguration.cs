namespace Rabbitus.Configuration
{
    public class RabbitusConfiguration
    {
        public string RabbitHost { get; set; }
        public int RabbitPort { get; set; }
        public string QueueName { get; set; }

        public RabbitusConfiguration()
        {
            RabbitHost = "localhost";
            RabbitPort = 1572;
        }
    }
}