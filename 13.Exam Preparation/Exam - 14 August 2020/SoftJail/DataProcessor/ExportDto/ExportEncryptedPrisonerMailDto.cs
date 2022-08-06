namespace SoftJail.DataProcessor.ExportDto
{

    using System.Xml.Serialization;

    [XmlType("Message")]
    public class ExportEncryptedPrisonerMailDto
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
