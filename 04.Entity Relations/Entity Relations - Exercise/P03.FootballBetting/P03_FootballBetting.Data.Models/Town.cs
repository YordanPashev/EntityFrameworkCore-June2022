namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using P03_FootballBetting.Data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Town
    {
        public Town()
        {
            Teams = new HashSet<Team>();
        }

        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(GlobalConst.TownNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
