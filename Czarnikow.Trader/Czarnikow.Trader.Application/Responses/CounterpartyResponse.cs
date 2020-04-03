namespace Czarnikow.Trader.Application.Responses
{
    using System.Text.Json.Serialization;

    public class CounterpartyResponse
    {
        [JsonPropertyName("counterpartyId")]
        public int CounterpartyId
        {
            get; set;
        }

        [JsonPropertyName("name")]
        public string Name
        {
            get; set;
        }
    }
}