using Givn;
using NUnit.Framework;
using Shouldly;

namespace Rabbitus.Tests.ConfigurationScenarios
{
    [TestFixture]
    public class CreatingBusDefaultConfigurationScenario
    {
        private IBus _bus;

        protected void CreateABusWithDefaultConfiguration()
        {
            _bus = Bus.Create(c => { });
        }

        protected void TheRabbitHostIsLocalhost()
        {
            _bus.Configuration.RabbitHost.ShouldBe("localhost");
        }

        protected void TheRabbitPortIs1572()
        {
            _bus.Configuration.RabbitPort.ShouldBe(1572);
        }

        [Test]
        public void Execute()
        {
            Wh.n(CreateABusWithDefaultConfiguration);
            Th.n(TheRabbitHostIsLocalhost)
                .And(TheRabbitPortIs1572);
        }
    }
}