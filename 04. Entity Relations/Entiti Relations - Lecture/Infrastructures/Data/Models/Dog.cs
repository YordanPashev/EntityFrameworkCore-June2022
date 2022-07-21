namespace Infrastructures.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Dog
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Person))]
        [Required]
        public int PersonId { get; set; }
    }
}
