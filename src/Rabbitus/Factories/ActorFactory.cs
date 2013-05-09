namespace Rabbitus.Factories
{
    public static class ActorFactory
    {
        public static IActorFactory Current { get; private set; }

        static ActorFactory()
        {
            Current = new DefaultActorFactory();
        }

        public static void SetCurrentActorFactory(IActorFactory actorFactory)
        {
            Current = actorFactory;
        }
    }
}