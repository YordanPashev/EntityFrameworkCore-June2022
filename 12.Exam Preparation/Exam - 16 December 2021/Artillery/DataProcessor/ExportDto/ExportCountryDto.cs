namespace Artillery.DataProcessor.ExportDto
{

    using System.Xml.Serialization;

    [XmlType("Country")]
    public class ExportCountryDto
    {
        [XmlAttribute("Country")]
        public string Countryname { get; set; }

        [XmlAttribute("ArmySize")]
        public int ArmySize { get; set; }
    }
}
