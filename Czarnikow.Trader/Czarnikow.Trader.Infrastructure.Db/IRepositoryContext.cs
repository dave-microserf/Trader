namespace Czarnikow.Trader.Infrastructure.Db
{
    using Czarnikow.Trader.Core.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IRepositoryContext
    {
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<Counterparty> Counterparties
        {
            get;
        }

        DbSet<Trade> Trades
        {
            get;
        }
    }
}