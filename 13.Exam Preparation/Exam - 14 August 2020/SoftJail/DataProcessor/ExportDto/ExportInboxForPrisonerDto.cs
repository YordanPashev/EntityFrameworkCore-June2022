namespace SoftJail.DataProcessor.ExportDto
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Prisoner")]
    public class ExportInboxForPrisonerDto
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public ExportEncryptedPrisonerMailDto[] EncryptedMessages { get; set; }

    }
}
