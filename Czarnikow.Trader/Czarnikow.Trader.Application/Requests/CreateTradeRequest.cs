namespace Czarnikow.Trader.Application.Requests
{
    using System;
    using System.Text.Json.Serialization;

    public class CreateTradeRequest
    {
        [JsonPropertyName("counterpartyId")]
        public int CounterpartyId
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