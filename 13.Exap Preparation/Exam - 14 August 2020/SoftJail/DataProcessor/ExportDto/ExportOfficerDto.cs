namespace SoftJail.DataProcessor.ExportDto
{
    using Newtonsoft.Json;

    [JsonObject]
    public class ExportOfficerDto
    {
        [JsonProperty("OfficerName")]
        public string Name { get; set; }
   
        [JsonProperty("Department")]
        public string Department { get; set; }
    }
}
