namespace Artillery.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    using Artillery.Common;

    [JsonObject]
    public class ImportGunDto
    {
        [JsonProperty("ManufacturerId")]
        [Required]
        public int ManufacturerId { get; set; }
       
        [JsonProperty("GunWeight")]
        [Required]
        [Range(EntityValidations.MinGunWeight, EntityValidations.MaxGunWeight)]
        public int GunWeight { get; set; }

        [JsonProperty("BarrelLength")]
        [Required]
        [Range(EntityValidations.MinBarrelLength, EntityValidations.MaxBarrelLength)]
        public double BarrelLength { get; set; }

        [JsonProperty("NumberBuild")]
        public int? NumberBuild { get; set; }

        [JsonProperty("Range")]
        [Required]
        [Range(EntityValidations.GunMinRange, EntityValidations.GunMaxRange)]
        public int Range { get; set; }

        [JsonProperty("GunType")]
        [Required]
        public string GunType { get; set; }

        [JsonProperty("ShellId")]
        [Required]
        public int ShellId { get; set; }

        [JsonProperty("Countries")]
        public List<ImportCountryIdDto> Countries { get; set; } = new List<ImportCountryIdDto>();
    }
}
