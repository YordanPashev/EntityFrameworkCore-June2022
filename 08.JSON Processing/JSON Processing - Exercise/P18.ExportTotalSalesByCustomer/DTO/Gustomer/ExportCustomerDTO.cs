namespace CarDealer.DTO.Gustomer
{
    using System;
    using System.Globalization;

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportCustomerDTO
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("BirthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("IsYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
