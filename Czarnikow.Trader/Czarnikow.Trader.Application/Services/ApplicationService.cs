namespace Czarnikow.Trader.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Interfaces;
    using Czarnikow.Trader.Application.Requests;
    using Czarnikow.Trader.Application.Responses;
    using Czarnikow.Trader.Core.Domain;
    using Czarnikow.Trader.Core.Interfaces;

    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork unitOfWork;

        public ApplicationService(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<CounterpartyResponse>> GetCounterpartiesAsync()
        {
            var list = await this.unitOfWork.CounterpartyRepository.ListAsync();
            
            return list.Select(
                counterparty => new CounterpartyResponse
                { 
                    CounterpartyId = counterparty.Id.Value, 
                    Name = counterparty.Name 
                }).ToList();
        }

        public async Task<List<CounterpartyTradeResponse>> GetCounterpartyTradesAsync(int counterpartyId)
        {
            var list = await this.unitOfWork.QueryRepository.GetTradesForCounterpartyIdAsync(counterpartyId);

            return list.Select(
                tuple => new CounterpartyTradeResponse
                {
                    CounterpartyId = tuple.Item1.Id.Value,
                    CounterpartyName = tuple.Item1.Name,
                    TradeId = tuple.Item2.Id.Value,
                    Product = tuple.Item2.Product,
                    Quantity = tuple.Item2.Quantity,
                    Price = tuple.Item2.Price,
                    Date = tuple.Item2.Date,
                    Direction = (string)(Direction)tuple.Item2.Direction
                }).ToList();
        }

        public async Task<TradeResponse> GetTradeAsync(int tradeId)
        {
            var trade = await this.unitOfWork.TradeRepository.FindAsync(tradeId);

            if (trade == null)
            {
                return null;
            }

            return new TradeResponse
            {
                TradeId = trade.Id.Value,
                CounterpartyId = trade.CounterpartyId,
                Product = trade.Product,
                Quantity = trade.Quantity,
                Price = trade.Price,
                Date = trade.Date,
                Direction = (string)(Direction)trade.Direction
            };
        }

        public async Task<int> CreateTradeAsync(CreateTradeRequest request)
        {
            var trade = Trade.Create(
                null,
                request.CounterpartyId,
                request.Product,
                request.Quantity,
                request.Price,
                request.Date,
                (Direction)request.Direction);
            
            this.unitOfWork.TradeRepository.Add(trade);
            await this.unitOfWork.SaveChangesAsync();
            return trade.Id.Value;
        }

        public async Task<bool> UpdateTradeAsync(UpdateTradeRequest request)
        {
            var trade = Trade.Create(
                request.TradeId,
                request.CounterpartyId,
                request.Product,
                request.Quantity,
                request.Price,
                request.Date,
                (Direction)request.Direction);

            this.unitOfWork.TradeRepository.Replace(trade);

            var result = await this.unitOfWork.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> DeleteTradeAsync(int tradeId)
        {
            var trade = await this.unitOfWork.TradeRepository.FindAsync(tradeId);

            if (trade == null)
            {
                return false;
            }
            
            this.unitOfWork.TradeRepository.Remove(trade);
            var result = await this.unitOfWork.SaveChangesAsync();
            return result == 1;
        }
    }
}