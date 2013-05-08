using Rabbitus.Tests.TestMessages;

namespace Rabbitus.Tests.TestActors
{
    public class TestActor : Actor<TestActor>
    {
        static TestActor()
        {
            ForMessage<TestMessage>()
                .HandledBy((actor, context) => actor.HandleTestMessage(context));
        }

        public void HandleTestMessage(IMessageContext<TestMessage> context)
        {
            context.Message.Received = true;
        }
    }
}