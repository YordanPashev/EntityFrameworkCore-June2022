namespace CarDealer.Dtos.Export
{

    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportCarsWithListOfPartsDTO
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        
        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravalledDistance { get; set; }

        [XmlArray("parts")]
        public ExportPartNameAndPriceDTO[] Parts { get; set; }
    }
}