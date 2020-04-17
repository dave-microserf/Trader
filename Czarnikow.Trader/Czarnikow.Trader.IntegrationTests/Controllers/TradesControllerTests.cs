namespace Czarnikow.Trader.IntegrationTests.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Api;
    using Czarnikow.Trader.Application.Requests;
    using Czarnikow.Trader.Application.Responses;
    using Czarnikow.Trader.Core.Domain;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    public class TradesControllerTests
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
        public async Task GetTradeId1_ShouldReturnSuccessAndTradeResponse_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/trades/1");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var utf8Json = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var trade = await JsonSerializer.DeserializeAsync<TradeResponse>(utf8Json);

            Assert.IsNotNull(trade);

            Assert.AreEqual(1, trade.TradeId);
            Assert.AreEqual(1, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(400.50m, trade.Price);
            Assert.AreEqual(new DateTime(2018, 1, 31), trade.Date);
            Assert.AreEqual(Direction.Buy.Name, trade.Direction);
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
            var request = new CreateTradeRequest
            {
                CounterpartyId = 1,
                Product = "Sugar",
                Quantity = 100,
                Price = 400,
                Date = new DateTime(2020, 2, 25),
                Direction = "Sell"
            };

            HttpResponseMessage httpResponseMessage;
            var bytes = JsonSerializer.SerializeToUtf8Bytes(request);

            var encoding = Encoding.UTF8;
            var json = encoding.GetString(bytes);
            var content = new StringContent(json, encoding, "application/json");

            httpResponseMessage = await this.client.PostAsync("/api/trades", content);

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, httpResponseMessage.StatusCode);

            var requestUri = httpResponseMessage.Headers.Location;

            httpResponseMessage = await this.client.GetAsync(requestUri);
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var utf8Json = new MemoryStream(Encoding.UTF8.GetBytes(responseContent));

            var trade = await JsonSerializer.DeserializeAsync<TradeResponse>(utf8Json);
            Assert.IsNotNull(trade);

            Assert.AreEqual(1, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(400m, trade.Price);
            Assert.AreEqual(new DateTime(2020, 2, 25), trade.Date);
            Assert.AreEqual(Direction.Sell.Name, trade.Direction);
        }

        [Test]
        public async Task Put_ShouldReturnCreated_Async()
        {
            var request = new UpdateTradeRequest
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
            var bytes = JsonSerializer.SerializeToUtf8Bytes(request);

            var encoding = Encoding.UTF8;
            var json = encoding.GetString(bytes);
            var content = new StringContent(json, encoding, "application/json");

            httpResponseMessage = await this.client.PutAsync("/api/trades", content);

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);

            httpResponseMessage = await this.client.GetAsync("/api/trades/1");
            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var utf8Json = new MemoryStream(Encoding.UTF8.GetBytes(responseContent));

            var trade = await JsonSerializer.DeserializeAsync<TradeResponse>(utf8Json);
            Assert.IsNotNull(trade);

            Assert.AreEqual(1, trade.TradeId);
            Assert.AreEqual(1, trade.CounterpartyId);
            Assert.AreEqual("Sugar", trade.Product);
            Assert.AreEqual(100, trade.Quantity);
            Assert.AreEqual(400m, trade.Price);
            Assert.AreEqual(new DateTime(2020, 2, 25), trade.Date);
            Assert.AreEqual(Direction.Sell.Name, trade.Direction);
        }

        [Test]
        public async Task PutTradeId99_ShouldReturnNotFound_Async()
        {
            var request = new UpdateTradeRequest
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
            var bytes = JsonSerializer.SerializeToUtf8Bytes(request);

            var encoding = Encoding.UTF8;
            var json = encoding.GetString(bytes);
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
    }
}