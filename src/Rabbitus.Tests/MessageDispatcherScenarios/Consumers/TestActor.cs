namespace Rabbitus.Tests.MessageDispatcherScenarios.Consumers
{
    public class TestActor : Actor<TestActor>
    {
        static TestActor()
        {
            ForMessage<FooMessage>()
                .HandledBy((actor, context) => actor.HandleTestMessage(context));
        }

        public void HandleTestMessage(IMessageContext<TestMessage> context)
        {
            context.Message.Received = true;
        }
    }
}