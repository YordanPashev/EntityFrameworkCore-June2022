namespace TeisterMask.DataProcessor.ImportDto
{

    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using TeisterMask.Common;

    [XmlType("Project")]
    public class ImportProjectDto
    {
        [XmlElement("Name")]
        [MinLength(EntityValidations.ProjectNameMinLength)]
        [MaxLength(EntityValidations.ProjectNameMaxLength)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string? DueDate { get; set; }

        [XmlArray("Tasks")]
        public virtual ImportTasksDto[] Tasks { get; set; }
    }
}
