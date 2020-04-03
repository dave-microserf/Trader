namespace Czarnikow.Trader.IntegrationTests.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Api;
    using Czarnikow.Trader.Application.Responses;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    [TestFixture]
    public class CounterpartiesControllerTests
    {
        private HttpClient client;
        private IntegrationTestStrategy strategy;

        [SetUp]
        public void Setup()
        {
            this.strategy = new IntegrationTestStrategy();

            var builder = new WebHostBuilder()
                .ConfigureServices(serviceCollection => serviceCollection.AddSingleton<IDbContextOptionsStrategy>(this.strategy))
                .UseStartup<Startup>();

            var testServer = new TestServer(builder);
            this.client = testServer.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            this.strategy?.Dispose();
            this.client?.Dispose();
        }

        [Test]
        public async Task GetCounterparties_ShouldReturnSuccessAndTwoCounterpartyResponse_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/counterparties");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var utf8Json = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var list = await JsonSerializer.DeserializeAsync<List<CounterpartyResponse>>(utf8Json);

            Assert.AreEqual(2, list.Count);

            ResponseAssert.IsCounterpartyId1(list.SingleOrDefault(item => item.CounterpartyId == 1));
            ResponseAssert.IsCounterpartyId2(list.SingleOrDefault(item => item.CounterpartyId == 2));
        }

        [Test]
        public async Task GetTradesForCounterpartyId1_ShouldReturnSuccessAndOneCounterpartyTradeResponse_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/counterparties/1/trades");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var utf8Json = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var list = await JsonSerializer.DeserializeAsync<List<CounterpartyTradeResponse>>(utf8Json);

            Assert.AreEqual(1, list.Count);

            ResponseAssert.IsCounterpartyTradeId1(list.First());
        }

        [Test]
        public async Task GetTradesForCounterpartyId2_ShouldReturnSuccessAndOneCounterpartyTradeResponse_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/counterparties/2/trades");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var utf8Json = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var list = await JsonSerializer.DeserializeAsync<List<CounterpartyTradeResponse>>(utf8Json);

            Assert.AreEqual(1, list.Count);

            ResponseAssert.IsCounterpartyTradeId2(list.First());
        }
    }
}