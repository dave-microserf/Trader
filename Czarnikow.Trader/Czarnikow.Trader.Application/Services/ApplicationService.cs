namespace Czarnikow.Trader.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Api;
    using Czarnikow.Trader.Application.Interfaces;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;

    public class ApplicationService : IApplicationService
    {
        public ApplicationService(
            IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<int> CreateTradeAsync(CreateTrade request)
        {
            var trade = new Trade.Builder
            {
                Date = request.Date,
                CounterpartyId = request.CounterpartyId,
                Product = request.Product,
                Quantity = request.Quantity,
                Price = request.Price,
                Direction = (Direction)request.Direction
            }.Build();
            
            this.UnitOfWork.TradeRepository.Insert(trade);
            await this.UnitOfWork.SaveChangesAsync();
            return trade.Id.Value;
        }

        public async Task<bool> UpdateTradeAsync(UpdateTrade request)
        {
            var trade = new Trade.Builder
            {
                Id = request.TradeId,
                Date = request.Date,
                CounterpartyId = request.CounterpartyId,
                Product = request.Product,
                Quantity = request.Quantity,
                Price = request.Price,
                Direction = (Direction)request.Direction
            }.Build();

            this.UnitOfWork.TradeRepository.Update(trade);

            var result = await this.UnitOfWork.SaveChangesAsync();
            return result == 1;
        }
        public async Task<bool> DeleteTradeAsync(int tradeId)
        {
            var trade = await this.UnitOfWork.TradeRepository.FindAsync(tradeId);

            if (trade == null)
            {
                return false;
            }

            this.UnitOfWork.TradeRepository.Delete(trade);
            var result = await this.UnitOfWork.SaveChangesAsync();
            return result == 1;
        }
    }
}