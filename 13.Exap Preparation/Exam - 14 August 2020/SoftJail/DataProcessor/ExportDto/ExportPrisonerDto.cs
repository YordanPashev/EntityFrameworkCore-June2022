namespace SoftJail.DataProcessor.ExportDto
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportPrisonerDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("CellNumber")]
        public int CellNumber { get; set; }

        [JsonProperty("Officers")]
        public ExportOfficerDto[] Officers { get; set; }

        [JsonProperty("TotalOfficerSalary")]
        public decimal TotalOfficerSalary { get; set; }
    }
}
