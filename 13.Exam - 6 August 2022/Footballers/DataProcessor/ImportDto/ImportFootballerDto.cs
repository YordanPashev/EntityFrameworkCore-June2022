namespace Footballers.DataProcessor.ImportDto
{

    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Footballers.Common;

    [XmlType("Footballer")]
    public class ImportFootballerDto
    {
        [XmlElement("Name")]
        [MinLength(EntityValidations.FootballerNameMinLength)]
        [MaxLength(EntityValidations.FootballerNameMaxLength)]
        public string Name { get; set; }

        [XmlElement("ContractStartDate")]
        [Required(AllowEmptyStrings = false)]
        public string ContractStartDate { get; set; }

        [XmlElement("ContractEndDate")]
        [Required(AllowEmptyStrings = false)]
        public string ContractEndDate { get; set; }

        [XmlElement("BestSkillType")]
        [Required(AllowEmptyStrings = false)]
        public int BestSkillType { get; set; }

        [XmlElement("PositionType")]
        [Required(AllowEmptyStrings = false)]
        public int PositionType { get; set; }

    }
}
