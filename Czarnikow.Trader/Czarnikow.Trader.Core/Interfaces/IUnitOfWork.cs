namespace Czarnikow.Trader.Core.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        IQueryRepository QueryRepository
        {
            get;
        }

        ICounterpartyRepository CounterpartyRepository
        { 
            get; 
        }

        ITradeRepository TradeRepository
        {
            get;
        }
    }
}