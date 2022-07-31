namespace CarDealer.DTO.Gustomer
{

    using Newtonsoft.Json;

    [JsonObject]
    public  class ExportCustomerFinalResult
    {
        [JsonProperty("fullName")]
        public string CustomerFullName { get; set; }

        [JsonProperty("boughtCars")]
        public int CarsBoughtCount { get; set; }

        [JsonProperty("spentMoney")]
        public decimal MoneySpent { get; set; }
    }
}
