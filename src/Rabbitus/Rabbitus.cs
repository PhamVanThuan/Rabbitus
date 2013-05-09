namespace Rabbitus
{
    public class Rabbitus : IRabbitus
    {
        internal Rabbitus()
        {
        }

        public void Publish<TMessage>(TMessage message) 
            where TMessage : class
        {
        }
    }
}