namespace Czarnikow.Trader.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Requests;
    using Czarnikow.Trader.Application.Responses;

    public interface IApplicationService
    {
        Task<List<CounterpartyResponse>> GetCounterpartiesAsync();

        Task<List<CounterpartyTradeResponse>> GetCounterpartyTradesAsync(int counterpartyId);

        Task<TradeResponse> GetTradeAsync(int tradeId);

        Task<int> CreateTradeAsync(CreateTradeRequest request);

        Task<bool> UpdateTradeAsync(UpdateTradeRequest request);

        Task<bool> DeleteTradeAsync(int tradeId);
    }
}
