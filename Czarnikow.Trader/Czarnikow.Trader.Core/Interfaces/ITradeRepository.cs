namespace Czarnikow.Trader.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;

    public interface ITradeRepository
    {
        Task<Trade> FindAsync(int tradeId);

        Task<List<Trade>> FindByCounterpartyAsync(int counterpartyId);

        void Insert(Trade trade);

        void Update(Trade trade);

        void Delete(Trade trade);

        Task<List<Trade>> ListAsync();
    }
}