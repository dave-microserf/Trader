namespace Czarnikow.Trader.Core.Domain
{
    public sealed class Direction
    {
        public static readonly Direction Buy = new Direction('B', "Buy");
        public static readonly Direction Sell = new Direction('S', "Sell");

        private Direction(char identifier, string name)
        {
            this.Identifier = identifier;
            this.Name = name;
        }

        public static explicit operator Direction(char identifier)
        {
            return identifier == Buy.Identifier ? Buy : identifier == Sell.Identifier ? Sell : null;
        }

        public static explicit operator Direction(string name)
        {
            return name == Buy.Name ? Buy : name == Sell.Name ? Sell : null;
        }
        
        public char Identifier
        {
            get;
        }

        public string Name 
        { 
            get; 
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}