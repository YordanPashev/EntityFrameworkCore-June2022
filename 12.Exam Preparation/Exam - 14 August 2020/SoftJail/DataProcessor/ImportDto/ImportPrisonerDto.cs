namespace SoftJail.DataProcessor.ImportDto
{

    using System;

    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoftJail.Common;

    [JsonObject]
    public class ImportPrisonerDto
    {
        [Required]
        [MinLength(GlobalConstants.PrisonerFullNameMinLength)]
        [MaxLength(GlobalConstants.PrisonerFullNameMaxLength)]
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [Required]
        [JsonProperty("Nickname")]
        [RegularExpression(GlobalConstants.PrisonerNickNameRegex)]
        public string Nickname { get; set; }

        [Required]
        [Range(GlobalConstants.PrisonerMinAge, GlobalConstants.PrisonerMaxAge)]
        [JsonProperty("Age")]
        public int Age { get; set; }

        [Required]
        [JsonProperty("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [JsonProperty("ReleaseDate")]
        public string? ReleaseDate { get; set; }

        [JsonProperty("Bail")]
        [Range(GlobalConstants.BailMinAmount, GlobalConstants.BailMaxAmount)]
        public decimal? Bail { get; set; }

        [JsonProperty("CellId")]
        [Range(GlobalConstants.CellNumberMin, GlobalConstants.CellNumberMax)]
        public int? CellId { get; set; }

        [JsonProperty("Mails")]
        public ICollection<ImportMailDto> Mails { get; set; } = new List<ImportMailDto>();
    }
}
