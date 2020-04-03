namespace Czarnikow.Trader.Core.UnitTests.Domain
{
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    class CountepartyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_ShouldSetProperties()
        {
            var counterparty = new Counterparty(1, "Company A");

            Assert.AreEqual(1, counterparty.Id);
            Assert.AreEqual("Company A", counterparty.Name);
        }
    }
}
