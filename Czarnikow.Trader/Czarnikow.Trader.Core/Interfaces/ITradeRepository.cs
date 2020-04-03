namespace Czarnikow.Trader.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;

    public interface ITradeRepository
    {
        Task<Trade> FindAsync(int tradeId);

        void Add(Trade trade);

        void Remove(Trade trade);

        void Replace(Trade trade);

        Task<List<Trade>> ListAsync();
    }
}