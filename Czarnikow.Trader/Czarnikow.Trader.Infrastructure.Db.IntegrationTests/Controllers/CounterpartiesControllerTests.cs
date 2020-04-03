namespace Czarnikow.Trader.Infrastructure.Db.IntegrationTests.Controllers
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
    using NUnit.Framework;

    public class CounterpartiesControllerTests
    {
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            var testServer = new TestServer(builder);
            this.client = testServer.CreateClient();
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

            CounterpartyResponseAssert.IsCounterpartyId1(list.SingleOrDefault(item => item.CounterpartyId == 1));
            CounterpartyResponseAssert.IsCounterpartyId2(list.SingleOrDefault(item => item.CounterpartyId == 2));
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

            CounterpartyTradeResponseAssert.IsCounterpartyTradeId1(list.First());
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

            CounterpartyTradeResponseAssert.IsCounterpartyTradeId2(list.First());
        }
    }
}