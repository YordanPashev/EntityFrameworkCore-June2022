namespace ProductShop.Dtos.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using ProductShop.Models;

    [XmlType("User")]
    public class ExportUserSoldProductsInfoDTO
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public ExportSoldProductDTOcs[] ProductsSold { get; set; }
    }
}
