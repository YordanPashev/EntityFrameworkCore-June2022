namespace Infrastructures.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Continent { get; set; }

        [StringLength(50)]
        [Required]
        public string Country { get; set; }

        [StringLength(50)]
        [Required]
        public string Town { get; set; }

        [StringLength(50)]
        [Required]
        public string FullAddress { get; set; }
    }
}
