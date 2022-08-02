namespace ProductShop.Dtos.Export
{

    using System.Xml.Serialization;

    [XmlType("SoldProducts")]
    public class ExportSoldProductsCountDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public ExportProductNameAndPriceDTO[] Products { get; set; }
    }
}
