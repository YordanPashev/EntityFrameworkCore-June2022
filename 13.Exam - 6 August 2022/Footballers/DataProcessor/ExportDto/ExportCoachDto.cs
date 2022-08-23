namespace Footballers.DataProcessor.ExportDto
{

    using System.Xml.Serialization;

    [XmlType("Coach")]
    public class ExportCoachDto
    {
        [XmlAttribute("FootballersCount")]
        public int FootballersCount { get; set; }

        [XmlElement()]
        public string CoachName { get; set; }

        [XmlArray("Footballers")]
        public ExportFootballerDto[] Footballers { get; set; }
    }
}
