namespace Czarnikow.Trader.Core.Domain
{
    using System;

    public class Trade : Entity<int?>
    {
        public static Trade Create(int? tradeId, int counterpartyId, string product, int quantity, decimal price, DateTime date, Direction direction)
        {
            if (tradeId.GetValueOrDefault(1) <= 0)
            {
                throw new ArgumentException(nameof(tradeId));
            }

            if (counterpartyId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(counterpartyId));
            }

            if (string.IsNullOrEmpty(product) || product.Length > 200)
            {
                throw new ArgumentException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            if (direction == null)
            {
                throw new ArgumentNullException(nameof(direction));
            }

            var trade = new Trade
            {
                Id = tradeId,
                CounterpartyId = counterpartyId,
                Product = product,
                Quantity = quantity,
                Price = price,
                Date = date,
                Direction = (char)direction,
            };

            return trade;
        }

        public int CounterpartyId
        {
            get; private set;
        }

        public string Product
        {
            get; private set;
        }

        public int Quantity
        {
            get; private set;
        }

        public decimal Price
        {
            get; private set;
        }

        public DateTime Date
        {
            get; private set;
        }

        public char Direction
        {
            get; private set;
        }

    }
}