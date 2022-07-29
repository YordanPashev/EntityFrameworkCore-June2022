namespace ProductShop.DTOs.CategoryProduct
{

    using System.ComponentModel.DataAnnotations;

    public class ImportCategoryProductDTO
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
