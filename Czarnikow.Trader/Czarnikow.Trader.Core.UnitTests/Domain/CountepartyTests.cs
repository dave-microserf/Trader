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
        public void Constructor_WhenNew()
        {
            var counterparty = new Counterparty(null, "New Company"); 

            Assert.IsNull(counterparty.Id);
            Assert.AreEqual("New Company", counterparty.Name);
        }

        [Test]
        public void Constructor_WhenExists()
        {
            var counterparty = new Counterparty(1, "Company A");

            Assert.AreEqual(1, counterparty.Id);
            Assert.AreEqual("Company A", counterparty.Name);
        }
    }
}