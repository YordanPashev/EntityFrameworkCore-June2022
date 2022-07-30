namespace ProductShop.DTOs.Product
{
    using Newtonsoft.Json;
    using ProductShop.DTOs.User;
    using System.Linq;

    [JsonObject]
    public class ExportProductCountAndListOfProductsDTO
    {
        [JsonProperty("count")]
        public int ProductsCount => ProductsSold.Any() 
                                    ? ProductsSold.Count()
                                    : 0;                

        [JsonProperty("products")]
        public ExportProductNameAndPriceDTO[] ProductsSold { get; set; }
    }
}
