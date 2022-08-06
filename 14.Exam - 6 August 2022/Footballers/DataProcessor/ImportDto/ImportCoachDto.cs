namespace Footballers.DataProcessor.ImportDto
{
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;

    using Footballers.Common;

    [XmlType("Coach")]
    public class ImportCoachDto
    {
        [XmlElement("Name")]
        [Required(AllowEmptyStrings = false)]
        [MinLength(EntityValidations.CoachNameMinLength)]
        [MaxLength(EntityValidations.CoachNameMaxLength)]
        public string Name { get; set; }

        [XmlElement("Nationality")]

        [Required(AllowEmptyStrings = false)]
        public string Nationality { get; set; }

        [XmlArray("Footballers")]
        public ImportFootballerDto[] Footballers { get; set; }
    }
}
