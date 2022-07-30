namespace ProductShop.DTOs.Category
{
    using Newtonsoft.Json;
    using ProductShop.Common;
    using System.ComponentModel.DataAnnotations;

    [JsonObject]
    public class ImportCategoryDTO
    {
        [JsonProperty("name")]
        [Required]
        [MinLength(GlobalConstants.CategoryNameMinLength), MaxLength(GlobalConstants.CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}
