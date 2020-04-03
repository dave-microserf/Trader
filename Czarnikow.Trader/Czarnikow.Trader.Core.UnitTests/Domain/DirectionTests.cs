namespace Czarnikow.Trader.Core.UnitTests.Domain
{
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    class DirectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BuyExplicitCharOperator_ShouldReturnB()
        {
            char ch = (char)Direction.Buy;
            Assert.AreEqual('B', ch);            
        }

        [Test]
        public void SellExplicitCharOperator_ShouldReturnS()
        {
            char ch = (char)Direction.Sell;
            Assert.AreEqual('S', ch);
        }
    }
}
