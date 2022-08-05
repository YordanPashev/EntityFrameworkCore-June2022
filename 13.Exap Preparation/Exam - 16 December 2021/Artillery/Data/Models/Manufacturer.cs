namespace Artillery.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Artillery.Common;

    public class Manufacturer
    {
        public Manufacturer()
        {
            Guns = new List<Gun>(); ;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidations.ManufacturerNameMaxLength)]
        public string ManufacturerName { get; set; }

        [Required]
        [MaxLength(EntityValidations.FoundedTextMaxLength)]
        public string Founded { get; set; }

        public virtual ICollection<Gun> Guns { get; set; }
    }
}
