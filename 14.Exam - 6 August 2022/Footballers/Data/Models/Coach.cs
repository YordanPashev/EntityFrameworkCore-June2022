namespace Footballers.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Footballers.Common;

    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidations.CoachNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Nationality { get; set; }

        public ICollection<Footballer> Footballers { get; set; } = new List<Footballer>();
    }
}
