namespace Rabbitus
{
    public class Actor<TActor>
        where TActor : Actor<TActor>
    {
        public static ActorMessageHandler<TActor, TMessage> ForMessage<TMessage>()
        {
            return ActorConfiguration<TActor>.ForMessage<TMessage>();
        }
    }
}