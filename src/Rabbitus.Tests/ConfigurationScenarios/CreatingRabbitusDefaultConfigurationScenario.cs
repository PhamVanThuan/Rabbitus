using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace Rabbitus.Tests.ConfigurationScenarios
{
    public class CreatingRabbitusDefaultConfigurationScenario
    {
        private IRabbitus _rabbitus;

        protected void WhenCreateRabbitusWithDefaultConfiguration()
        {
            _rabbitus = RabbitusFactory.Configure(c => { });
        }

        protected void ThenTheRabbitHostIsLocalhost()
        {
            _rabbitus.Configuration.RabbitHost.ShouldBe("localhost");
        }

        protected void AndTheRabbitPortIs1572()
        {
            _rabbitus.Configuration.RabbitPort.ShouldBe(1572);
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }
}