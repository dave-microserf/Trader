namespace Czarnikow.Trader.IntegrationTests.Controllers
{
    using System;
    using Czarnikow.Trader.Application.Responses;
    using Czarnikow.Trader.Core.Domain;
    using NUnit.Framework;

    public static class ResponseAssert
    {
        public static void IsCounterpartyId1(CounterpartyResponse result)
        {
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.CounterpartyId);
            Assert.AreEqual("Company A", result.Name);
        }

        public static void IsCounterpartyId2(CounterpartyResponse result)
        {
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.CounterpartyId);
            Assert.AreEqual("Company B", result.Name);
        }

        public static void IsCounterpartyTradeId1(CounterpartyTradeResponse result)
        {
            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.CounterpartyId);
            Assert.AreEqual("Company A", result.CounterpartyName);
            Assert.AreEqual(1, result.TradeId);
            Assert.AreEqual(1, result.CounterpartyId);
            Assert.AreEqual("Sugar", result.Product);
            Assert.AreEqual(100, result.Quantity);
            Assert.AreEqual(400.50m, result.Price);
            Assert.AreEqual(new DateTime(2018, 1, 31), result.Date);
            Assert.AreEqual(Direction.Buy.Name, result.Direction);
        }

        public static void IsCounterpartyTradeId2(CounterpartyTradeResponse result)
        {
            Assert.IsNotNull(result);

            Assert.AreEqual(2, result.CounterpartyId);
            Assert.AreEqual("Company B", result.CounterpartyName);
            Assert.AreEqual(2, result.TradeId);
            Assert.AreEqual(2, result.CounterpartyId);
            Assert.AreEqual("Sugar", result.Product);
            Assert.AreEqual(100, result.Quantity);
            Assert.AreEqual(450.10m, result.Price);
            Assert.AreEqual(new DateTime(2018, 3, 31), result.Date);
            Assert.AreEqual(Direction.Sell.Name, result.Direction);
        }
    }
}