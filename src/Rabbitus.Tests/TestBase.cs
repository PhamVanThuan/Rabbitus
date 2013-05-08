using NUnit.Framework;
using TestStack.BDDfy;

namespace Rabbitus.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public abstract void ScenarioSetup();

        [Test]
        public virtual void Execute()
        {
            this.BDDfy();
        } 
    }
}