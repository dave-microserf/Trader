namespace Czarnikow.Trader.Infrastructure.Db.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class QueryRepository : IQueryRepository
    {
        private readonly IRepositoryContext context;

        public QueryRepository(IRepositoryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Tuple<Counterparty, Trade>>> GetTradesForCounterpartyIdAsync(int counterpartyId)
        {
            var query = from trade in this.context.Trades
                        where trade.CounterpartyId == counterpartyId
                        join counterparty in this.context.Counterparties on trade.CounterpartyId equals counterparty.Id
                        select Tuple.Create(counterparty, trade);

            return await query.ToListAsync();
        }
    }
}