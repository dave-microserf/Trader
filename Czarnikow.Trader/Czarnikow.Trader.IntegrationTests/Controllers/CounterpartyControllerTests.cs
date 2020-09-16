namespace Czarnikow.Trader.IntegrationTests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Api;
    using Czarnikow.Trader.Core.Domain;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NUnit.Framework;

    [TestFixture]
    public class CounterpartyControllerTests
    {
        private HttpClient client;
        private IntegrationTestOptionsStrategy strategy;

        [SetUp]
        public void Setup()
        {
            this.strategy = new IntegrationTestOptionsStrategy();

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
        public async Task GetCounterparties_ShouldReturnSuccessAndTwoCounterparties_Async()
        {
            var httpResponseMessage = await this.client.GetAsync("/api/counterparties");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            Assert.IsTrue(httpResponseMessage.IsSuccessStatusCode);

            var settings = new JsonSerializerSettings { ContractResolver = PrivateResolver.Default };
            var list = JsonConvert.DeserializeObject<List<Counterparty>>(content, settings);

            Assert.AreEqual(2, list.Count);

            CounterpartyAssert.IsCounterpartyId1(list.SingleOrDefault(item => item.Id == 1));
            CounterpartyAssert.IsCounterpartyId2(list.SingleOrDefault(item => item.Id == 2));
        }
    }

    public class PrivateResolver : DefaultContractResolver
    {
        public static readonly PrivateResolver Default = new PrivateResolver();

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                var hasPrivateSetter = property?.GetSetMethod(true) != null;
                prop.Writable = hasPrivateSetter;
            }
            return prop;
        }
    }
}