﻿namespace CarDealer.DTO.Car
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportCarShortInfoDTO
    {
        [JsonProperty("Make")]
        public string Make { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("TravelledDistance")]
        public long TravelledDistance { get; set; }
    }
}
