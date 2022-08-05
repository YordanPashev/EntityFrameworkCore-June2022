namespace Artillery.DataProcessor.ImportDto
{

    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using Artillery.Common;

    [XmlType("Shell")]
    public class ImportShellDto
    {
        [XmlElement("ShellWeight")]
        [Required]
        [Range(EntityValidations.MinShellWeight, EntityValidations.MaxShellWeight)]
        public double ShellWeight { get; set; }

        [XmlElement("Caliber")]
        [Required]
        [MinLength(EntityValidations.MinCaliberLength)]
        [MaxLength(EntityValidations.MaxCaliberLength)]
        public string Caliber { get; set; }
    }
}
