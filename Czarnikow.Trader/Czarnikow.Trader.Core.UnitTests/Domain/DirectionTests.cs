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
        public void WhenBChar_ExplicitOperatorDirection_ShouldReturnBuy()
        {
            var actual = (Direction)'B';
            Assert.AreSame(Direction.Buy, actual);
        }

        [Test]
        public void WhenBuyString_ExplicitOperatorDirection_ShouldReturnBuy()
        {
            var actual = (Direction)"Buy";
            Assert.AreSame(Direction.Buy, actual);
        }

        [Test]
        public void WhenSChar_ExplicitOperatorDirection_ShouldReturnSell()
        {
            var actual = (Direction)'S';
            Assert.AreSame(Direction.Sell, actual);
        }

        [Test]
        public void WhenSellString_ExplicitOperatorDirection_ShouldReturnSell()
        {
            var actual = (Direction)"Sell";
            Assert.AreSame(Direction.Sell, actual);
        }
        
        [Test]
        public void WhenBuy_Identifier_ShouldBeB()
        {
            Assert.AreEqual('B', Direction.Buy.Identifier);
        }

        [Test]
        public void WhenSell_Identifier_ShouldBeS()
        {
            Assert.AreEqual('S', Direction.Sell.Identifier);
        }

        [Test]
        public void WhenBuy_Name_ShouldBeBuy()
        {
            Assert.AreEqual("Buy", Direction.Buy.Name);
        }

        [Test]
        public void WhenSell_Name_ShouldBeSell()
        {
            Assert.AreEqual("Sell", Direction.Sell.Name);
        }

        [Test]
        public void WhenBuy_ToString_ShouldBeBuy()
        {
            Assert.AreEqual("Buy", Direction.Buy.ToString());
        }

        [Test]
        public void WhenSell_ToString_ShouldBeSell()
        {
            Assert.AreEqual("Sell", Direction.Sell.ToString());
        }
    }
}