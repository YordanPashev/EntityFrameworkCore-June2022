namespace MusicHub.Data.Models
{
    using MusicHub.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Writer
    {
        public Writer()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConst.WriterNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(GlobalConst.PseudonymMaxLength)]
        public string Pseudonym { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
