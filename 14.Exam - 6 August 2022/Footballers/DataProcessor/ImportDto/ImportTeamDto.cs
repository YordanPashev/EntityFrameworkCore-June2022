namespace Footballers.DataProcessor.ImportDto
{
    using Footballers.Common;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    [JsonObject]
    public class ImportTeamDto
    {
        [JsonProperty("Name")]
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(EntityValidations.TeamNameRegex)]
        [MinLength(EntityValidations.TeamNameMinLength)]
        [MaxLength(EntityValidations.TeamNameMaxLength)]
        public string Name { get; set; }

        [JsonProperty("Nationality")]
        [Required(AllowEmptyStrings = false)]
        [MinLength(EntityValidations.NationalityMinLength)]
        [MaxLength(EntityValidations.NationalityMaxLength)]
        public string Nationality { get; set; }

        [JsonProperty("Trophies")]
        [Required(AllowEmptyStrings = false)]
        [Range(EntityValidations.MinThropiesCount, EntityValidations.MaxThropiesCount)]
        public int Trophies { get; set; }

        [JsonProperty("Footballers")]
        public int[] Footballers { get; set; }
    }
}
