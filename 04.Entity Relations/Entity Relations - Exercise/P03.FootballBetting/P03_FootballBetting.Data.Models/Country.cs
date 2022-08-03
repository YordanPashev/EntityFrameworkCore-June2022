namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using P03_FootballBetting.Data.Common;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            Towns = new HashSet<Town>();
        }

        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(GlobalConst.CountryMaxLength)]
        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}
