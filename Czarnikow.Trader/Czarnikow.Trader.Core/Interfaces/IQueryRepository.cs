namespace Czarnikow.Trader.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Core.Domain;

    public interface IQueryRepository
    {
        Task<List<Tuple<Counterparty, Trade>>> GetTradesForCounterpartyIdAsync(int counterpartyId);
    }
}