namespace Czarnikow.Trader.Core.UnitTests.Domain
{
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    class TradeTests
    {
        public void Builder_WhenNew()
        {
            var trade = new Trade.Builder()
            {
                 
            }.Build();

            Assert.IsNotNull(trade);
        }
    }
}
