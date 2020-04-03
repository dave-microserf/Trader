namespace Czarnikow.Trader.Infrastructure.Db.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TradeRepository : ITradeRepository
    {
        private readonly IRepositoryContext context;
        
        public TradeRepository(IRepositoryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Trade> FindAsync(int tradeId)
        {            
            return await this.context.Trades.FindAsync(tradeId);
        }

        public void Add(Trade trade)
        {
            this.context.Trades.Add(trade);
        }

        public void Remove(Trade trade)
        {
            this.context.Trades.Remove(trade);
        }

        public void Replace(Trade trade)
        {
            this.context.Trades.Attach(trade);
            this.context.Entry(trade).State = EntityState.Modified;
        }

        public async Task<List<Trade>> ListAsync()
        {
            return await this.context.Trades.ToListAsync();
        }
    }
}