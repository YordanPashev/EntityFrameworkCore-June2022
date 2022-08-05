namespace Artillery.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Artillery.Data.Models.Enums;

    public class Gun
    {
        public Gun()
        {
            CountriesGuns = new List<CountryGun>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        public int GunWeight { get; set; }

        [Required]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        public int Range { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [ForeignKey(nameof(Shell))]
        public int ShellId { get; set; }
        public virtual Shell Shell { get; set; }

        public ICollection<CountryGun> CountriesGuns { get; set; }
    }
}
