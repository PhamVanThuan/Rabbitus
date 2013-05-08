using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace Rabbitus.Tests.MessageDispatcherScenarios
{
    [TestFixture]
    public class DispatchMessageScenario
    {
        private MessageDispatcher _dispatcher;
        private TestMessage _message;

        [SetUp]
        public void ScenarioSetup()
        {
            _dispatcher = new MessageDispatcher();
        }

        protected void GivenAMessage()
        {
            _message = new TestMessage();
        }

        protected void AndGivenARegisteredActor()
        {
           _dispatcher.RegisterActor<TestActor>();
        }

        protected void WhenDispatchingTheMessage()
        {
            _dispatcher.Dispatch(new MessageContext<TestMessage>(_message));
        }

        protected void ThenTheMessageIsDispatchedToTheActor()
        {
            _message.Received.ShouldBe(true);
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
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