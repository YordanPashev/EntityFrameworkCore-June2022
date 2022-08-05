namespace Artillery.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Artillery.Common;

    [XmlType("Country")]
    public class ImportCountryDto
    {
        [XmlElement("CountryName")]
        [Required]
        [MinLength(EntityValidations.CountryNameMinLength)]
        [MaxLength(EntityValidations.CountryNameMaxLength)]
        public string CountryName { get; set; }

        [XmlElement("ArmySize")]
        [Required]
        [Range(EntityValidations.MinArmySize, EntityValidations.MaxArmySize)]
        public int ArmySize { get; set; }
    }
}
