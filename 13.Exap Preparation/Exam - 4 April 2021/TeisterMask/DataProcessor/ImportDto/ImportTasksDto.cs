namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    using TeisterMask.Common;

    [XmlType("Task")]
    public class ImportTasksDto
    {
        [XmlElement("Name")]
        [MinLength(EntityValidations.TaskNameMinLength)]
        [MaxLength(EntityValidations.TaskNameMaxLength)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlElement("ExecutionType")]
        public int ExecutionType { get; set; }

        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}
