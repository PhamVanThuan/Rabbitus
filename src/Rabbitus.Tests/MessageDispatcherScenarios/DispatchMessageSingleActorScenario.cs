using NUnit.Framework;
using Rabbitus.Context;
using Rabbitus.Factories;
using Rabbitus.InboundDispatcher;
using Rabbitus.InboundDispatcher.Builders;
using Rabbitus.Tests.TestActors;
using Rabbitus.Tests.TestMessages;
using Shouldly;
using TestStack.BDDfy;

namespace Rabbitus.Tests.MessageDispatcherScenarios
{
    [TestFixture]
    public class DispatchMessageSingleActorScenario
    {
        private IInboundMessageDispatcher _dispatcher;
        private TestMessage _message;
        private DefaultActorFactory _actorFactory;

        [SetUp]
        public void ScenarioSetup()
        {
            _actorFactory = new DefaultActorFactory();
        }

        public void GivenAnInboundDispatcherCreatedWithRegisteredActorTypes()
        {
            var builder = new InboundMessageDispatcherBuilder(_actorFactory, new[] {typeof (TestActor)});
            _dispatcher = builder.Build();
        }

        protected void AndGivenAMessage()
        {
            _message = new TestMessage();
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