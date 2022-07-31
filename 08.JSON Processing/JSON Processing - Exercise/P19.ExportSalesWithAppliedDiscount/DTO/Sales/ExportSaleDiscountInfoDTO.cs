

namespace CarDealer.DTO.Sales
{

    using CarDealer.DTO.Car;

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportSaleDiscountInfoDTO
    {
        [JsonProperty("car")]
        public ExportCarShortInfoDTO Car { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("Discount")]
        public string Discount { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("priceWithDiscount")]
        public string PriceWithDiscount { get; set; }
    }
}
