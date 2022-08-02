namespace ProductShop.Dtos.Export
{
    
    using System.Xml.Serialization;

    [XmlType("Users")]
    public class ExportUsersAndTheirCountDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportUserProductsDTO[] Users { get; set; }
    }
}
