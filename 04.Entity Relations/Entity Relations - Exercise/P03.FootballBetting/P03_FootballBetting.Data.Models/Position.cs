namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using P03_FootballBetting.Data.Common;
    using System.ComponentModel.DataAnnotations;

    public class Position
    {
        public Position()
        {
            Players = new HashSet<Player>();
        }

        [Key]
        public int PositionId { get; set; }

        [Required]
        [MaxLength(GlobalConst.PositionNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
