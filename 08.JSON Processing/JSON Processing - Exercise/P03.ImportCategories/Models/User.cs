namespace ProductShop.Models
{
    using ProductShop.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public User()
        {
            this.ProductsSold = new List<Product>();
            this.ProductsBought = new List<Product>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.UserLastNameMinLength)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        [InverseProperty(nameof(Product.Seller))]
        public ICollection<Product> ProductsSold { get; set; }

        [InverseProperty(nameof(Product.Buyer))]
        public ICollection<Product> ProductsBought { get; set; }
    }
}