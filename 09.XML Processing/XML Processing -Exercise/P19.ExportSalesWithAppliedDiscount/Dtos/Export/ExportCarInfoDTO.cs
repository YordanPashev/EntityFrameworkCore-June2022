namespace CarDealer.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportCarInfoDTO
    {
        [XmlAttribute("make")]
        public string Car { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
