namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using P03_FootballBetting.Data.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Player
    {
        public Player()
        {
            PlayerStatistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int PlayerId { get; set; }
        
        [Required]
        [MaxLength(GlobalConst.PlayerNameMaxLength)]
        public string Name { get; set; }

        public byte SquadNumber { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public bool IsInjured { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
