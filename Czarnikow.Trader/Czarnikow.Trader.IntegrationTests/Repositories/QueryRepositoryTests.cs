namespace Czarnikow.Trader.IntegrationTests.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Api;
    using Czarnikow.Trader.Core.Interfaces;
    using Czarnikow.Trader.Infrastructure.Db.EntityFramework;
    using Czarnikow.Trader.Infrastructure.Db.Repositories;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    public class QueryRepositoryTests
    {
        private RepositoryContext context;
        private IQueryRepository repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(Startup.ConnectionString)
                .Options;
            
            this.context = new RepositoryContext(options);
            this.repository = new QueryRepository(this.context);
        }

        [Test]
        public async Task GetTradesForCounterpartyIdAsync_ShouldReturnCounterpartyAndTradeForCounterpartyId1_Async()
        {
            var tupleList = await this.repository.GetTradesForCounterpartyIdAsync(1);

            var tuple = tupleList.SingleOrDefault(tuple => tuple.Item1.Id == 1);

            CounterpartyAssert.IsCounterpartyId1(tuple.Item1);
            TradeAssert.IsTradeId1(tuple.Item2);
        }

        [Test]
        public async Task GetTradesForCounterpartyIdAsync_ShouldReturnCounterpartyAndTradeForCounterpartyId2_Async()
        {
            var tupleList = await this.repository.GetTradesForCounterpartyIdAsync(2);

            var tuple = tupleList.SingleOrDefault(tuple => tuple.Item1.Id == 2);

            CounterpartyAssert.IsCounterpartyId2(tuple.Item1);
            TradeAssert.IsTradeId2(tuple.Item2);
        }
    }
}
