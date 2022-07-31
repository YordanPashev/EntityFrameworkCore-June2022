namespace CarDealer.DTO.Gustomer
{

    using System;

    using Newtonsoft.Json;

    [JsonObject]
    public class ImportCustomerDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
