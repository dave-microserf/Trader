namespace Czarnikow.Trader.Core.Domain
{
    using System;

    public class Trade : Entity<int?>
    {
        private Trade()
        {       
        }

        public class Builder : Asserter
        {
            public int? Id
            {
                get; set;
            }

            public DateTime Date
            {
                get; set;
            }

            public int CounterpartyId 
            { 
                get; set;
            }

            public string Product
            { 
                get; set; 
            }

            public int Quantity
            {
                get; set;
            }

            public decimal Price
            {
                get; set;
            }

            public Direction Direction
            {
                get; set;
            }

            public Trade Build()
            {
                this.AddIfLessThan(nameof(this.CounterpartyId), this.CounterpartyId, 1);
                ////this.AddIfLengthLessThan(nameof(this.Prod))

                ////if (string.IsNullOrEmpty(product) || product.Length > 200)
                ////{
                ////    throw new ArgumentException(nameof(product));
                ////}

                ////if (quantity <= 0)
                ////{
                ////    throw new ArgumentOutOfRangeException(nameof(quantity));
                ////}

                ////if (price <= 0)
                ////{
                ////    throw new ArgumentOutOfRangeException(nameof(price));
                ////}

                ////if (direction == null)
                ////{
                ////    throw new ArgumentNullException(nameof(direction));
                ////}

                return new Trade()
                {
                    Id = this.Id,
                    CounterpartyId = this.CounterpartyId,
                    Product = this.Product,
                    Quantity = this.Quantity,
                    Price = this.Price,
                    Date = this.Date,
                    Direction = this.Direction.Identifier
                };
            }
        }

        public DateTime Date
        {
            get; private set;
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

        public char Direction
        {
            get; private set;
        }

        public Counterparty Counterparty
        {
            get; private set;
        }
    }
}