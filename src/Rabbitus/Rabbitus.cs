namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        public Rabbitus()
        {
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
        }
    }
}