using Givn;
using NUnit.Framework;

namespace Rabbitus.Tests.MessageDispatcherScenarios
{
    [TestFixture]
    public class DispatchMessageScenario
    {
        private readonly MessageDispatcher _dispatcher;
        private TestMessage _message;
        
        public DispatchMessageScenario()
        {
            _dispatcher = new MessageDispatcher();
        }

        protected void AMessage()
        {
            _message = new TestMessage();
        }

        protected void ARegisteredActor()
        {
           _dispatcher.RegisterActor<TestActor>();
        }

        protected void DispatchingTheMessage()
        {
            _dispatcher.Dispatch(new MessageContext<TestMessage>(_message));
        }

        protected void TheMessageIsDispatchedToTheActor()
        {
            _message.Received = true;
        }

        [Test]
        public void Execute()
        {
            Giv.n(AMessage)
                .And(ARegisteredActor);
            Wh.n(DispatchingTheMessage);
            Th.n(TheMessageIsDispatchedToTheActor);
        }

        protected class TestMessage
        {
            public bool Received { get; set; }
        }

        protected class TestActor : Actor<TestActor>
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
}