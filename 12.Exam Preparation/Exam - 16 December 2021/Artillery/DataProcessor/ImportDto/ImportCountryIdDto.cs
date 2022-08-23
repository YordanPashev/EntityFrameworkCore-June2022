namespace Artillery.DataProcessor.ImportDto
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ImportCountryIdDto
    {
        [JsonProperty("Id")]
        public int CountryId { get; set; }
    }
}
