namespace Czarnikow.Trader.Infrastructure.Db.IntegrationTests.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;
    using Czarnikow.Trader.Infrastructure.Db.EntityFramework;
    using Czarnikow.Trader.Infrastructure.Db.Repositories;
    using NUnit.Framework;

    public class TradeRepositoryTests
    {
        private RepositoryContext context;
        private ITradeRepository repository;

        [SetUp]
        public void Setup()
        {
            this.context = new RepositoryContext();
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

        [Test, Rollback]
        public async Task Add_ShouldInsertAsync()
        {
            var trade = Trade.Create(null, 1, "Sugar", 100, 100, DateTime.Now, Direction.Buy);

            this.repository.Add(trade);
            await this.context.SaveChangesAsync();

            var inserted = await this.repository.FindAsync(trade.Id.Value);

            Assert.AreEqual(trade.Id, inserted.Id);
            Assert.AreEqual(trade.CounterpartyId, inserted.CounterpartyId);
            Assert.AreEqual(trade.Product, inserted.Product);
            Assert.AreEqual(trade.Quantity, inserted.Quantity);
            Assert.AreEqual(trade.Price, inserted.Price);
            Assert.AreEqual(trade.Date, inserted.Date);
            Assert.AreEqual(trade.Direction, inserted.Direction);
        }

        [Test, Rollback]
        public async Task Remove_ShouldDelete_Async()
        {
            var trade = await this.repository.FindAsync(1);
            
            this.repository.Remove(trade);
            await this.context.SaveChangesAsync();

            var deleted = await this.repository.FindAsync(1);

            Assert.IsNull(deleted);
        }

        [Test, Rollback]
        public async Task Set_ShouldUpdate_Async()
        {
            var trade = Trade.Create(1, 2, "Spice", 100, 100, DateTime.Now, Direction.Sell);

            this.repository.Set(trade);
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
    }
}