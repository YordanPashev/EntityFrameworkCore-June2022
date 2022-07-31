namespace CarDealer.DTO.Part
{

    using Newtonsoft.Json;

    [JsonObject]
    public  class ExportPartNameAndPriceDTO
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Price")]
        public string Price { get; set; }
    }
}
