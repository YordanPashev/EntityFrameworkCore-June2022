namespace CarDealer.Dtos.Export
{

    using System.Xml.Serialization;

    [XmlType("suplier")]
    public class ExportLocalSupplierDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("parts-count")]
        public long PartsCount { get; set; }
    }
}