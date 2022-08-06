namespace Footballers.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Footballers.Common;

    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidations.TeamNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EntityValidations.NationalityMaxLength)]
        public string Nationality { get; set; }

        [Required]
        public int Trophies { get; set; }

        public virtual ICollection<TeamFootballer> TeamsFootballers { get; set; } = new List<TeamFootballer>();
    }
}
