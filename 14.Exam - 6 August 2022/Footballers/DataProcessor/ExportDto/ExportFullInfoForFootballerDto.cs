namespace Footballers.DataProcessor.ExportDto
{

    using System;
    using Newtonsoft.Json;

    [JsonObject]
    public class ExportFullInfoForFootballerDto
    {
        [JsonProperty("FootballerName")]
        public string FootballerName { get; set; }

        [JsonProperty("ContractStartDate")]
        public string ContractStartDate { get; set; }

        [JsonProperty("ContractEndDate")]
        public string ContractEndDate { get; set; }

        [JsonProperty("BestSkillType")]
        public string BestSkillType { get; set; }


        [JsonProperty("PositionType")]
        public string PositionType { get; set; }
    }
}
