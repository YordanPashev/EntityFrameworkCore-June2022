namespace Artillery.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Artillery.Common;

    [XmlType("Manufacturer")]
    public class ImportManufactorerDto
    {
        [XmlElement("ManufacturerName")]
        [Required]
        [MinLength(EntityValidations.ManufacturerNameMinLength)]
        [MaxLength(EntityValidations.ManufacturerNameMaxLength)]
        public string ManufacturerName { get; set; }

        [XmlElement("Founded")]
        [Required]
        [MinLength(EntityValidations.FoundedTextMinLength)]
        [MaxLength(EntityValidations.FoundedTextMaxLength)]
        public string Founded { get; set; }
    }
}
