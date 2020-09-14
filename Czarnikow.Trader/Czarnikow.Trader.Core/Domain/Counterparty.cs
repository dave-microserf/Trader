namespace Czarnikow.Trader.Core.Domain
{
    using System;

    public class Counterparty : Entity<int?>
    {        
        private Counterparty()
        {
        }

        public Counterparty(int? counterpartyId, string name)
        {
            if (counterpartyId.GetValueOrDefault(1) <= 0)
            {
                throw new ArgumentException(nameof(counterpartyId));
            }

            if (string.IsNullOrEmpty(name) || name.Length > 200)
            {
                throw new ArgumentException(nameof(name));
            }
            
            this.Id = counterpartyId;
            this.Name = name;
        }
        
        public string Name
        {
            get; private set;
        }
    }
}