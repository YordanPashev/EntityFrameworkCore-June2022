namespace CarDealer.Dtos.Export
{

    using System.Xml.Serialization;

    [XmlType("customer")]
    public class ExportCustomerSalesDTO
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }
        
        [XmlAttribute("bought-cars")]
        public int CarsBought { get; set; }

        [XmlAttribute("spent-money")]
        public decimal MoneySpent { get; set; }
    }
}