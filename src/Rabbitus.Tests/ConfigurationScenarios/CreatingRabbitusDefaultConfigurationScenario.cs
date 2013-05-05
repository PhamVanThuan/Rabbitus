using Givn;
using NUnit.Framework;
using Shouldly;

namespace Rabbitus.Tests.ConfigurationScenarios
{
    [TestFixture]
    public class CreatingRabbitusDefaultConfigurationScenario
    {
        private IRabbitus _rabbitus;

        protected void CreateRabbitusWithDefaultConfiguration()
        {
            _rabbitus = RabbitusFactory.Configure(c => { });
        }

        protected void TheRabbitHostIsLocalhost()
        {
            _rabbitus.Configuration.RabbitHost.ShouldBe("localhost");
        }

        protected void TheRabbitPortIs1572()
        {
            _rabbitus.Configuration.RabbitPort.ShouldBe(1572);
        }

        [Test]
        public void Execute()
        {
            Wh.n(CreateRabbitusWithDefaultConfiguration);
            Th.n(TheRabbitHostIsLocalhost)
                .And(TheRabbitPortIs1572);
        }
    }
}