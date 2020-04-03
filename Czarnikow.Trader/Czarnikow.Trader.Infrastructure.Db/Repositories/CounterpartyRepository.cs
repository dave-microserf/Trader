namespace Czarnikow.Trader.Infrastructure.Db.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CounterpartyRepository : ICounterpartyRepository
    {
        private readonly IRepositoryContext context;

        public CounterpartyRepository(IRepositoryContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));            
        }

        public async Task<Counterparty> FindAsync(int counterpartyId)
        {
            return await this.context.Counterparties.FindAsync(counterpartyId);
        }

        public async Task<List<Counterparty>> ListAsync()
        {
            return await this.context.Counterparties.ToListAsync();
        }
    }
}