namespace Czarnikow.Trader.IntegrationTests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Api;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;
    using Czarnikow.Trader.Infrastructure.Db.EntityFramework;
    using Czarnikow.Trader.Infrastructure.Db.Repositories;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    public class TradeRepositoryTests
    {
        private TraderDbContext context;
        private ITradeRepository repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TraderDbContext>()
                .UseSqlServer(Startup.ConnectionString)
                .Options;

            this.context = new TraderDbContext(options);
            this.repository = new TradeRepository(this.context);
        }

        [Test]
        public async Task ListAsync_ShouldReturnTwoTrades_Async()
        {
            var tradeList = await this.repository.ListAsync();

            var trade = tradeList.Single(trade => trade.Id == 1);

            TradeAssert.IsTradeId1(trade);

            trade = tradeList.Single(trade => trade.Id == 2);

            TradeAssert.IsTradeId2(trade);
        }

        [Test]
        public async Task FindAsync_ShouldReturnTradeId1_Async()
        {
            var trade = await this.repository.FindAsync(1);

            TradeAssert.IsTradeId1(trade);
        }

        [Test]
        public async Task FindAsync_ShouldReturnTradeId2_Async()
        {
            var trade = await this.repository.FindAsync(2);

            TradeAssert.IsTradeId2(trade);
        }

        [Test]
        public async Task FindAsync_ShouldReturnNullForTradeId99_Async()
        {
            var trade = await this.repository.FindAsync(99);

            Assert.IsNull(trade);
        }


        [Test]
        public async Task FindByCounterpartyAsync_ShouldReturnTradesCounterpartyId1_Async()
        {
            var list = await this.repository.FindByCounterpartyAsync(1);

            var trade = list.SingleOrDefault(trade => trade.Id == 1);

            TradeAssert.IsTradeId1(trade);
        }

        [Test]
        public async Task FindByCounterpartyAsync_ShouldReturnTradesCounterpartyId2_Async()
        {
            var list = await this.repository.FindByCounterpartyAsync(2);

            var trade = list.SingleOrDefault(trade => trade.Id == 2);

            TradeAssert.IsTradeId2(trade);
        }

        [Test, Rollback]
        public async Task Insert_ShouldInsert_Async()
        {
            var trade = new Trade.Builder()
            {
                Date = DateTime.Now,
                CounterpartyId = 1,
                Product = "Sugar",
                Quantity = 100,
                Price = 100,
                Direction = Direction.Buy
            }.Build();

            this.repository.Insert(trade);
            await this.context.SaveChangesAsync();

            var inserted = await this.repository.FindAsync(trade.Id.Value);

            Assert.AreEqual(trade.Id, inserted.Id);
            Assert.AreEqual(trade.CounterpartyId, inserted.CounterpartyId);
            Assert.AreEqual(trade.Product, inserted.Product);
            Assert.AreEqual(trade.Quantity, inserted.Quantity);
            Assert.AreEqual(trade.Price, inserted.Price);
            Assert.AreEqual(trade.Date, inserted.Date);
            Assert.AreEqual(trade.Direction, inserted.Direction);

            Assert.IsNotNull(trade.Counterparty);
            Assert.AreEqual("Company A", trade.Counterparty.Name);
            Assert.AreEqual(1, trade.Counterparty.Id);
        }

        [Test, Rollback]
        public async Task Update_ShouldUpdate_Async()
        {
            var trade = new Trade.Builder
            {
                Id = 1,
                Date = DateTime.Now,
                CounterpartyId = 2,
                Product = "Spice",
                Quantity = 100,
                Price = 100,
                Direction = Direction.Sell
            }.Build();

            this.repository.Update(trade);
            await this.context.SaveChangesAsync();

            var updated = await this.repository.FindAsync(trade.Id.Value);

            Assert.AreEqual(trade.Id, updated.Id);
            Assert.AreEqual(trade.CounterpartyId, updated.CounterpartyId);
            Assert.AreEqual(trade.Product, updated.Product);
            Assert.AreEqual(trade.Quantity, updated.Quantity);
            Assert.AreEqual(trade.Price, updated.Price);
            Assert.AreEqual(trade.Date, updated.Date);
            Assert.AreEqual(trade.Direction, updated.Direction);
        }

        [Test, Rollback]
        public async Task Delete_ShouldDelete_Async()
        {
            var trade = await this.repository.FindAsync(1);

            this.repository.Delete(trade);
            await this.context.SaveChangesAsync();

            var deleted = await this.repository.FindAsync(1);

            Assert.IsNull(deleted);
        }
    }
}