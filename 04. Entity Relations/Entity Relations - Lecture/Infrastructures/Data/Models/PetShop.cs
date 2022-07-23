namespace Infrastructures.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PetShops")]
    public class PetShop
    {
        public PetShop()
        {
            People = new List<Person>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public ICollection<Person> People { get; set; }
    }
}
