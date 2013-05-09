using NUnit.Framework;
using Rabbitus.Tests.TestActors;
using Rabbitus.Tests.TestMessages;
using Shouldly;
using TestStack.BDDfy;

namespace Rabbitus.Tests.InboundMessageDispatcherScenarios
{
    /*[TestFixture]
    public class DispatchMessageSingleActorScenario
    {
        private TestMessage _message;
        private IRabbitus _bus;

        [SetUp]
        public void ScenarioSetup()
        {
            _bus = RabbitusFactory.Configure(c => { });
        }

        public void GivenASubscribedActor()
        {
            _bus.Subscribe<TestActor>();
        }

        protected void AndGivenAMessage()
        {
            _message = new TestMessage();
        }

        protected void WhenDispatchingTheMessage()
        {
            //_bus.Publish(_message);
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
    }*/
}