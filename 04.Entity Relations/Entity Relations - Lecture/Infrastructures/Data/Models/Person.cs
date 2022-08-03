namespace Infrastructures.Data.Models
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Person
    {
        public Person()
        {
            Dogs = new List<Dog>();
            PetShops = new List<PetShop>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Range(1, 120)]
        [Required]
        public int Age { get; set; }

        [ForeignKey(nameof(Dog))]
        public int? DogId { get; set; }
        public IList<Dog> Dogs { get; set; }

        [ForeignKey(nameof(PetShop))]
        public int PetShopId { get; set; }
        public ICollection<PetShop> PetShops { get; set; }

        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
