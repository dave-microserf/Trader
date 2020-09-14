namespace Czarnikow.Trader.Application.Interfaces
{
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Api;
    using Czarnikow.Trader.Core.Interfaces;

    public interface IApplicationService
    {
        IUnitOfWork UnitOfWork
        {
            get;
        }

        Task<int> CreateTradeAsync(CreateTrade request);

        Task<bool> UpdateTradeAsync(UpdateTrade request);

        Task<bool> DeleteTradeAsync(int tradeId);
    }
}
