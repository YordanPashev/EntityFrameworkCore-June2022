﻿namespace CarDealer.DTO.Sales
{

    using Newtonsoft.Json;

    [JsonObject]
    internal class ImportSaleDTO
    {
        [JsonProperty("carId")]
        public int CarId { get; set; }

        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }
    }
}
