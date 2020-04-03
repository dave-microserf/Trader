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

    public class CounterpartyRepositoryTests
    {
        private RepositoryContext context;
        private ICounterpartyRepository repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(Startup.ConnectionString)
                .Options;

            this.context = new RepositoryContext(options);
            this.repository = new CounterpartyRepository(this.context);
        }

        [Test]
        public async Task FindAsync_ShouldReturnCounterpartyForCounterpartyId1_Async()
        {
            var companyA = await this.repository.FindAsync(1);

            CounterpartyAssert.IsCounterpartyId1(companyA);
        }

        [Test]
        public async Task FindAsync_ShouldReturnCounterpartyForCounterpartyId2_Async()
        {
            var companyB = await this.repository.FindAsync(2);

            CounterpartyAssert.IsCounterpartyId2(companyB);
        }

        [Test]
        public async Task ListAsync_ShouldReturnTwoCounterparties_Async()
        {
            var counterpartyList = await this.repository.ListAsync();

            var companyA = counterpartyList.SingleOrDefault(counterparty => counterparty.Id == 1);

            CounterpartyAssert.IsCounterpartyId1(companyA);

            var companyB = counterpartyList.SingleOrDefault(counterparty => counterparty.Id == 2);

            CounterpartyAssert.IsCounterpartyId2(companyB);
        }
    }
}