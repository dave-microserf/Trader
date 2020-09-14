namespace Czarnikow.Trader.Infrastructure.Db
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Interfaces;
    using Czarnikow.Trader.Infrastructure.Db.EntityFramework;
    using Czarnikow.Trader.Infrastructure.Db.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TraderDbContext context;

        private readonly Lazy<ICounterpartyRepository> lazyCounterparyRepository;
        private readonly Lazy<ITradeRepository> lazyTradeRepository;        

        public UnitOfWork(TraderDbContext context)
        {
            this.context = context;
            this.lazyCounterparyRepository = new Lazy<ICounterpartyRepository>(() => new CounterpartyRepository(context));
            this.lazyTradeRepository = new Lazy<ITradeRepository>(() => new TradeRepository(context));
        }

        public ICounterpartyRepository CounterpartyRepository => this.lazyCounterparyRepository.Value;

        public ITradeRepository TradeRepository => this.lazyTradeRepository.Value;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return -1;
            }
        }
    }
}