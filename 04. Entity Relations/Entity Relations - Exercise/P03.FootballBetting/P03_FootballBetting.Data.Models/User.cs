namespace P03_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using P03_FootballBetting.Data.Common;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            Bets = new HashSet<Bet>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(GlobalConst.UserNameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(GlobalConst.PasswordMaxLength)]
        public string Password { get; set; }

        [Required]
        [MaxLength(GlobalConst.EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(GlobalConst.NameMaxLength)]
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
