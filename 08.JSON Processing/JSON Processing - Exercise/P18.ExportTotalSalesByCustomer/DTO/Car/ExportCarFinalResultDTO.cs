namespace CarDealer.DTO.Car
{
    using CarDealer.DTO.Part;
    using Newtonsoft.Json;

    [JsonObject]
    public class ExportCarFinalResultDTO
    {
        [JsonProperty("car")]
        public ExportCarShortInfoDTO Car { get; set; }

        [JsonProperty("parts")]
        public ExportPartNameAndPriceDTO[] Parts { get; set; }
    }
}
