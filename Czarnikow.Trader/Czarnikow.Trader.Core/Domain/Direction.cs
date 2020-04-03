namespace Czarnikow.Trader.Core.Domain
{
    public sealed class Direction
    {
        public static readonly Direction Buy = new Direction("Buy", 'B');
        public static readonly Direction Sell = new Direction("Sell", 'S');

        private Direction(string name, char identifier)
        {
            this.Name = name;
            this.Identifier = identifier;
        }

        public static explicit operator char(Direction direction)
        {
            return direction.Identifier;
        }

        public static explicit operator Direction(char identifier)
        {
            return identifier == Buy.Identifier ? Buy : identifier == Sell.Identifier ? Sell : null;
        }

        public static explicit operator string(Direction direction)
        {
            return direction.Name;
        }

        public static explicit operator Direction(string name)
        {
            return name == Buy.Name ? Buy : name == Sell.Name ? Sell : null;
        }

        public string Name 
        { 
            get; 
        }

        public char Identifier
        { 
            get; 
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}