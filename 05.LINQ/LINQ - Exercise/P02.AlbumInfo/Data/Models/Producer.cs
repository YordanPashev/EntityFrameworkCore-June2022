namespace MusicHub.Data.Models
{
    using MusicHub.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Producer
    {
        public Producer()
        {
            Albums = new HashSet<Album>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConst.ProducerNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(GlobalConst.PseudonymMaxLength)]
        public string Pseudonym { get; set; }

        [MaxLength(GlobalConst.ProducerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
