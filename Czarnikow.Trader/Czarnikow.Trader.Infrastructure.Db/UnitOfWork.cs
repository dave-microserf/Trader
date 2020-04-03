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
        private readonly Lazy<ICounterpartyRepository> lazyCounterparyRepository;
        private readonly Lazy<ITradeRepository> lazyTradeRepository;
        private readonly Lazy<IQueryRepository> lazyQueryRepository;
        
        private readonly RepositoryContext context;

        public UnitOfWork(RepositoryContext context)
        {
            this.lazyCounterparyRepository = new Lazy<ICounterpartyRepository>(() => new CounterpartyRepository(context));
            this.lazyTradeRepository = new Lazy<ITradeRepository>(() => new TradeRepository(context));
            this.lazyQueryRepository = new Lazy<IQueryRepository>(() => new QueryRepository(context));            
            this.context = context;
        }

        public ICounterpartyRepository CounterpartyRepository => this.lazyCounterparyRepository.Value;

        public ITradeRepository TradeRepository => this.lazyTradeRepository.Value;

        public IQueryRepository QueryRepository => this.lazyQueryRepository.Value;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // The task result contains the number of state entries written to the underlying database. 
                // This can include state entries for entities and/or relationships.
                return await this.context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                // A database command did not affect the expected number of rows.
                // This usually indicates an optimistic concurrency violation; that is, a row has been changed in the database since it was queried.
                return -1;
            }
        }
    }
}