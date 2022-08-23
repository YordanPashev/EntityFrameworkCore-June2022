namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    using SoftJail.Common;

    [JsonObject]
    public class ImportDepartmentDto
    {
        [JsonProperty("Name")]
        [MinLength(GlobalConstants.DepartmenNameMinLength)]
        [MaxLength(GlobalConstants.DepartmenNameMaxLength)]

        public string Name { get; set; }

        [JsonProperty("Cells")]
        public ICollection<ImportCellDto> Cells { get; set;} = new List<ImportCellDto>();
    }
}
