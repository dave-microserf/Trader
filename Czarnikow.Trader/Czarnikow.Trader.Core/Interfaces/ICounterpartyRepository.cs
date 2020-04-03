namespace Czarnikow.Trader.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;

    public interface ICounterpartyRepository
    {
        Task<Counterparty> FindAsync(int counterpartyId);

        Task<List<Counterparty>> ListAsync();
    }
}