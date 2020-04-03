namespace Czarnikow.Trader.Application.Responses
{
    using System;
    using System.Text.Json.Serialization;

    public class CounterpartyTradeResponse
    {
        [JsonPropertyName("counterpartyId")]
        public int CounterpartyId
        {
            get; set;
        }

        [JsonPropertyName("counterpartyName")]
        public string CounterpartyName
        {
            get; set;
        }

        [JsonPropertyName("tradeId")]
        public int TradeId
        {
            get; set;
        }

        [JsonPropertyName("product")]
        public string Product
        {
            get; set;
        }

        [JsonPropertyName("quantity")]
        public int Quantity
        {
            get; set;
        }

        [JsonPropertyName("price")]
        public decimal Price
        {
            get; set;
        }

        [JsonPropertyName("date")]
        public DateTime Date
        {
            get; set;
        }

        [JsonPropertyName("direction")]
        public string Direction
        {
            get; set;
        }
    }
}
