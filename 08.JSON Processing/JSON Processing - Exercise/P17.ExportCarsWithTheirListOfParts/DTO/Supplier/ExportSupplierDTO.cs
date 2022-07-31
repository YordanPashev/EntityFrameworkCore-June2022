namespace CarDealer.DTO.Supplier
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportSupplierDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("PartsCount")]
        public int PartsCount { get; set; }
    }
}
