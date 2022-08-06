namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using SoftJail.Common;
    using SoftJail.Data.Models.Enums;

    [XmlType("Officer")]
    public class ImportOfficerDto
    {
        [Required]
        [XmlElement("Name")]
        [MinLength(GlobalConstants.OfficerFullNameMinLength)]
        [MaxLength(GlobalConstants.OfficerFullNameMaxLength)]
        public string FullName { get; set; }

        [Required]
        [Range(GlobalConstants.SalaryMinAmount, GlobalConstants.SalaryMaxAmount)]
        [XmlElement("Money")]
        public decimal Salary { get; set; }

        [Required]
        [EnumDataType(typeof(Position))]
        [XmlElement("Position")]
        public string Position { get; set; }

        [Required]
        [EnumDataType(typeof(Weapon))]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        [Required]
        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public ImportOfficerPrisonerDto[] Prisoners { get; set; }
    }
}
