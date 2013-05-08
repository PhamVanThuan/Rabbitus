using NUnit.Framework;
using Rabbitus.Tests.TestActors;
using Rabbitus.Tests.TestMessages;
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

        protected void AndGivenASubscribedActor()
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
    }
}