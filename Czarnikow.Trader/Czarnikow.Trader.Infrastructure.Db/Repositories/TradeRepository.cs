namespace Czarnikow.Trader.Infrastructure.Db.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TradeRepository : ITradeRepository
    {
        private readonly ITraderDbContext context;
        
        public TradeRepository(ITraderDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Trade> FindAsync(int tradeId)
        {            
            return await this.context.Trades.Include(trade => trade.Counterparty).SingleOrDefaultAsync(trade => trade.Id == tradeId);
        }

        public async Task<List<Trade>> FindByCounterpartyAsync(int counterpartyId)
        {
            return await this.context.Trades.Include(trade => trade.Counterparty).Where(trade => trade.CounterpartyId == counterpartyId).ToListAsync();
        }

        public void Insert(Trade trade)
        {
            this.context.Trades.Add(trade);
        }

        public void Update(Trade trade)
        {
            this.context.Trades.Attach(trade);
            this.context.Entry(trade).State = EntityState.Modified;
        }

        public void Delete(Trade trade)
        {
            this.context.Trades.Remove(trade);
        }

        public async Task<List<Trade>> ListAsync()
        {
            return await this.context.Trades.Include(trade => trade.Counterparty).ToListAsync();
        }
    }
}