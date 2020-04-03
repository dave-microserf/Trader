namespace Czarnikow.Trader.IntegrationTests.Repositories
{
    using System;
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    public static class CounterpartyAssert
    {
        public static void IsCounterpartyId1(Counterparty counterparty)
        {
            Assert.IsNotNull(counterparty);

            Assert.AreEqual(1, counterparty.Id);
            Assert.AreEqual("Company A", counterparty.Name);
        }

        public static void IsCounterpartyId2(Counterparty counterparty)
        {
            Assert.IsNotNull(counterparty);

            Assert.AreEqual(2, counterparty.Id);
            Assert.AreEqual("Company B", counterparty.Name);
        }
    }

    public static class TradeAssert
    {
        public static void IsTradeId1(Trade trade)
        {
            Assert.IsNotNull(trade);

            Assert.AreEqual(1, trade.Id);
            Assert.AreEqual(1, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(400.50m, trade.Price);
            Assert.AreEqual(new DateTime(2018, 1, 31), trade.Date);
            Assert.AreEqual(Direction.Buy.Identifier, trade.Direction);
        }

        public static void IsTradeId2(Trade trade)
        {
            Assert.IsNotNull(trade);

            Assert.AreEqual(2, trade.Id);
            Assert.AreEqual(2, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(450.10m, trade.Price);
            Assert.AreEqual(new DateTime(2018, 3, 31), trade.Date);
            Assert.AreEqual(Direction.Sell.Identifier, trade.Direction);
        }
    }
}