namespace Czarnikow.Trader.IntegrationTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Api;
    using Czarnikow.Trader.Application.Api;
    using Czarnikow.Trader.Core.Domain;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using NUnit.Framework;

    [TestFixture]
    public class TradeControllerTests
    {
        private IntegrationTestOptionsStrategy strategy;
        private TestServer testServer;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            this.strategy = new IntegrationTestOptionsStrategy();

            var builder = new WebHostBuilder()
                .ConfigureServices(serviceCollection => serviceCollection.AddSingleton<IDbContextOptionsStrategy>(this.strategy))
                .UseStartup<Startup>();

            this.testServer = new TestServer(builder);
            this.client = testServer.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            this.client?.Dispose();
            this.strategy?.Dispose();
            this.testServer?.Dispose();
        }

        [Test]
        public async Task GetTradeId1_ShouldReturnSuccessAndTrade_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/trades/1");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var settings = new JsonSerializerSettings { ContractResolver = PrivateResolver.Default };
            var trade = JsonConvert.DeserializeObject<Trade>(content, settings);

            TradeAssert.IsTradeId1(trade);
        }

        [Test]
        public async Task GetTradeId99_ShouldReturnNotFound_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/trades/99");

            Assert.IsFalse(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponseMessage.StatusCode);
        }

        [Test]
        public async Task Post_ShouldReturnCreated_Async()
        {
            var request = new CreateTrade
            {
                CounterpartyId = 1,
                Product = "Sugar",
                Quantity = 100,
                Price = 400,
                Date = new DateTime(2020, 2, 25),
                Direction = "Sell"
            };

            HttpResponseMessage httpResponseMessage;
            var json = JsonConvert.SerializeObject(request, Formatting.Indented);

            var encoding = Encoding.UTF8;
            var content = new StringContent(json, encoding, "application/json");

            httpResponseMessage = await this.client.PostAsync("/api/trades", content);

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, httpResponseMessage.StatusCode);

            var requestUri = httpResponseMessage.Headers.Location;

            httpResponseMessage = await this.client.GetAsync(requestUri);
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings { ContractResolver = PrivateResolver.Default };
            var trade = JsonConvert.DeserializeObject<Trade>(responseContent, settings);

            Assert.IsNotNull(trade);

            Assert.AreEqual(1, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(400m, trade.Price);
            Assert.AreEqual(new DateTime(2020, 2, 25), trade.Date);
            Assert.AreEqual(Direction.Sell.Identifier, trade.Direction);
        }

        [Test]
        public async Task Put_ShouldReturnCreated_Async()
        {
            var request = new UpdateTrade
            {
                TradeId = 1,
                CounterpartyId = 1,
                Product = "Sugar",
                Quantity = 100,
                Price = 400,
                Date = new DateTime(2020, 2, 25),
                Direction = "Sell"
            };

            HttpResponseMessage httpResponseMessage;
            var json = JsonConvert.SerializeObject(request, Formatting.Indented);

            var encoding = Encoding.UTF8;
            var content = new StringContent(json, encoding, "application/json");

            httpResponseMessage = await this.client.PutAsync("/api/trades", content);

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);

            httpResponseMessage = await this.client.GetAsync("/api/trades/1");
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            
            var settings = new JsonSerializerSettings { ContractResolver = PrivateResolver.Default };
            var trade = JsonConvert.DeserializeObject<Trade>(responseContent, settings);

            Assert.IsNotNull(trade);

            Assert.AreEqual(1, trade.Id);
            Assert.AreEqual(1, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(400m, trade.Price);
            Assert.AreEqual(new DateTime(2020, 2, 25), trade.Date);
            Assert.AreEqual(Direction.Sell.Identifier, trade.Direction);
        }

        [Test]
        public async Task PutTradeId99_ShouldReturnNotFound_Async()
        {
            var request = new UpdateTrade
            {
                TradeId = 99,
                CounterpartyId = 1,
                Product = "Sugar",
                Quantity = 100,
                Price = 400,
                Date = new DateTime(2020, 2, 25),
                Direction = "Sell"
            };

            HttpResponseMessage httpResponseMessage;
            var json = JsonConvert.SerializeObject(request, Formatting.Indented);

            var encoding = Encoding.UTF8;
            var content = new StringContent(json, encoding, "application/json");

            httpResponseMessage = await this.client.PutAsync("/api/trades", content);

            Assert.IsFalse(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponseMessage.StatusCode);
        }

        [Test]
        public async Task Delete_ShouldReturnOk_Async()
        {
            var httpResponseMessage = await this.client.DeleteAsync("/api/trades/1");
            
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);
            
            var httpGetResponseMessage = await this.client.GetAsync("/api/trades/1");
            
            Assert.AreEqual(HttpStatusCode.NotFound, httpGetResponseMessage.StatusCode);
        }

        [Test]
        public async Task DeleteTradeId99_ShouldReturnNotFound_Async()
        {
            var httpResponseMessage = await this.client.DeleteAsync("/api/trades/99");

            Assert.IsFalse(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, httpResponseMessage.StatusCode);
        }

        [Test]
        public async Task GetTradesForCounterpartyId1_ShouldReturnSuccessAndOneTrade_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/trades?counterpartyId=1");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var settings = new JsonSerializerSettings { ContractResolver = new PrivateResolver() };
            var list = JsonConvert.DeserializeObject<List<Trade>>(content, settings);

            Assert.AreEqual(1, list.Count);

            TradeAssert.IsTradeId1(list.First());
        }

        [Test]
        public async Task GetTradesForCounterpartyId2_ShouldReturnSuccessAndOneTrade_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/trades?counterpartyId=2");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var settings = new JsonSerializerSettings { ContractResolver = new PrivateResolver() };
            var list = JsonConvert.DeserializeObject<List<Trade>>(content, settings);

            Assert.AreEqual(1, list.Count);

            TradeAssert.IsTradeId2(list.First());
        }
    }
}