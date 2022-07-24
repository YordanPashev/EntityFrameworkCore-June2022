namespace MusicHub.Data.Models
{
    using MusicHub.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Performer
    {
        public Performer()
        {
            PerformerSongs = new HashSet<SongPerformer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConst.PerformerFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GlobalConst.PerformerLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal NetWorth { get; set; }

        public virtual ICollection<SongPerformer> PerformerSongs { get; set; }

    }
}
