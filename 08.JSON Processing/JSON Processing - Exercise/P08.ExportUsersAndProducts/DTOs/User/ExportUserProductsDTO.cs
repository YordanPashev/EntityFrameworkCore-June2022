namespace ProductShop.DTOs.User
{

    using Newtonsoft.Json;
    using ProductShop.DTOs.Product;

    [JsonObject]
    public class ExportUserProductsDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("soldProducts")]
        public ExportProductCountAndListOfProductsDTO ProductsSold { get; set; }
    }
}
