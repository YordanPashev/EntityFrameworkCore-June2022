namespace Footballers.DataProcessor.ExportDto
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportTeamInfoDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Footballers")]
        public ExportFullInfoForFootballerDto[] Footballers { get; set; }
    }
}
