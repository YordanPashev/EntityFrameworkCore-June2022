namespace CarDealer.DTO.Part
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ImportPartDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("supplierId")]
        public int SupplierId { get; set; }
    }
}
