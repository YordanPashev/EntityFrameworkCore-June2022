namespace SoftJail.DataProcessor.ImportDto
{

    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    using SoftJail.Common;

    [JsonObject]
    public class ImportCellDto
    {
        [Required]
        [Range(GlobalConstants.CellNumberMin, GlobalConstants.CellNumberMax)]
        [JsonProperty("CellNumber")]
        public int CellNumber { get; set; }

        [Required]
        [JsonProperty("HasWindow")]
        public bool HasWindow { get; set; }
    }
}
